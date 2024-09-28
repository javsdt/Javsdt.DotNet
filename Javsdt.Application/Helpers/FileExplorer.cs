using HappreeTool.Documents;
using Javsdt.Shared.Configuration;
using Javsdt.Application.Helpers.Base;
using Javsdt.Domain.Entitys;
using Javsdt.Domain.Services;
using Javsdt.Shared.Enums;
using Javsdt.Shared.Utils.Metadata;
using Microsoft.Extensions.Logging;

namespace Javsdt.Application.Helper
{
    public class FileExplorer(ILogger<FileExplorer> logger, FileAnalyzer fileAnalyzer,
        JavService _javService, SubtitleService _subtitleService)
    {
        /// <summary>
        /// 是否每个车牌单独一个文件夹
        /// </summary>
        private readonly bool needSeparateFolder = SettingsHolder.NeedSeparateFolder;

        /// <summary>
        /// 收集指定目录下的所有jav视频和字幕
        /// </summary>
        /// <param name="rootDir"></param>
        public void CollectJavFilesInRootDir(string rootDir)
        {
            Stack<string> DirStack = new Stack<string>(); // 使用栈模拟递归的文件夹堆栈
            DirStack.Push(rootDir); // 将根文件夹入栈
            while (DirStack.Count > 0)
            {
                string currentDir = DirStack.Pop(); // 弹出栈顶文件夹
                logger.LogInformation("【收集jav】检索中...当前目录: {currentDir}", currentDir);

                //当前文件夹在排除文件夹中，则不整理
                if (IsCurrentFolderExclude(rootDir, currentDir, SettingsHolder.Standard.Birthmark.ExcludeFolders)) continue;

                //当前文件夹下的文件
                string[] files = Directory.GetFiles(currentDir);
                string[] subDirs = Directory.GetDirectories(currentDir);

                //1 收集【视频】
                List<Jav> javs = CollectJavs(files.Where(VideoUtils.IsVideoFile));

                //2 初步检查当前文件夹是否独立文件夹
                IsCurrentFoldeSeparate(subDirs, javs);

                //3 检查视频自身显而易见的特征，中字、破解等
                foreach (var jav in javs)
                {
                    fileAnalyzer.DiscoverObviousFeatures(jav);
                }

                //4 处理“CD”的问题，同一车牌同一版本有多少cd，用于cd1，cd2...的命名
                GroupAndSortJavs(javs);

                //5 收集【字幕】
                List<Subtitle> notBelongedSubtitles = CollectSubtitles(files.Where(VideoUtils.IsSubtitleFile), javs);
                _subtitleService.AddRange(notBelongedSubtitles);

                //Todo: 6 允许用户手工修正版本和CD

                //入库
                _javService.AddRange(javs);

                // 获取当前文件夹的子文件夹并入栈
                foreach (string subDir in subDirs)
                {
                    DirStack.Push(subDir);
                }
            }

            //如果用户归类的规则路径中包含车牌，则用户需要每个车牌指定一个独立文件夹。那么就不允许源jav散布在不同文件夹，标记error。
            if (needSeparateFolder)
            {
                _javService.UpdateInDifferentFoldersStatus();
            }
        }

        /// <summary>
        /// 检查当前文件夹是否被排除，无需整理
        /// </summary>
        /// <param name="rootDir"></param>
        /// <param name="currentDir"></param>
        /// <returns></returns>
        private static bool IsCurrentFolderExclude(string rootDir, string currentDir, List<string> excludeFolders)
        {
            string relativeDir = currentDir.Replace(rootDir, string.Empty);
            return excludeFolders.Any(relativeDir.Contains);
        }

        /// <summary>
        /// 从当前一级目录中收集Jav
        /// </summary>
        /// <param name="videoPaths"></param>
        /// <returns></returns>
        private List<Jav> CollectJavs(IEnumerable<string> videoPaths)
        {
            List<Jav> javs = [];  //当前一层目录下的所有JavFile
            foreach (string filePath in videoPaths)
            {
                if (TryCollectFc2Jav(filePath, out Jav? jav) ||
                    TryCollectCommonCarJav(filePath, out jav) ||
                    TryCollectTerribleCarJav(filePath, out jav))
                {
                    // 按照Fc2 => 有码/素人/ => 无码的顺序，若找到车牌则jav被赋值
                }
                else
                {
                    // 当找不到匹配时，创建一个未找到车牌的Jav对象
                    jav = Jav.NotFoundCar(filePath);
                }

                javs.Add(jav!);
            }

            return javs;
        }

        /// <summary>
        /// 尝试从绝对路径提取【FC2】类型的JavFile
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="javFile"></param>
        /// <returns></returns>
        private static bool TryCollectFc2Jav(string filePath, out Jav? javFile)
        {
            if (CarUtils.TryExtractFc2Car(Path.GetFileNameWithoutExtension(filePath), out string? car))
            {
                javFile = Jav.FoundCar(filePath, car!);
                return true;
            }

            javFile = null;
            return false;
        }

        /// <summary>
        /// 尝试从绝对路径提取【有码/素人】类型的JavFile
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="javFile"></param>
        /// <returns></returns>
        private bool TryCollectCommonCarJav(string filePath, out Jav? javFile)
        {
            if (CarUtils.TryExtractCommonCar(Path.GetFileNameWithoutExtension(filePath),
                SettingsHolder.Standard.Birthmark.IgnoredWords, out string? car))
            {
                javFile = Jav.FoundCar(filePath, car!);
                return true;
            }

            javFile = null;
            return false;
        }

        /// <summary>
        /// 尝试从绝对路径提取【无码】类型的JavFile
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="javFile"></param>
        /// <returns></returns>
        private bool TryCollectTerribleCarJav(string filePath, out Jav? javFile)
        {
            if (CarUtils.TryExtractTerribleCar(Path.GetFileNameWithoutExtension(filePath),
                SettingsHolder.Standard.Birthmark.IgnoredWords, out string? car))
            {
                javFile = Jav.FoundCar(filePath, car!);
                return true;
            }

            javFile = null;
            return false;
        }

        /// <summary>
        /// 为jav匹配唯一的字幕文件，收集无法确定归属的字幕
        /// </summary>
        /// <param name="subtitlePaths"></param>
        /// <param name="javs"></param>
        /// <returns></returns>
        /// <remarks>一个字幕文件的车牌有唯一的jav与之对应，则给这个jav.Subtitles收集这个字幕，如果有多个jav，则收集它。</remarks>
        private List<Subtitle> CollectSubtitles(IEnumerable<string> subtitlePaths, List<Jav> javs)
        {
            List<Subtitle> notBelongedSubtitles = [];  //当前一层目录下的所有JavFile
            foreach (string filePath in subtitlePaths)
            {
                if (TryCollectFc2Subtitle(filePath, out Subtitle? subtitle) ||
                    TryCollectCommonCarSubtitle(filePath, out subtitle) ||
                    TryCollectTerribleCarSubtitle(filePath, out subtitle))
                {
                    if (subtitle is null) continue;

                    // 在 javs 中查找与当前 subtitle 的 CarName 相匹配的 Jav
                    List<Jav> matchingJavs = javs.Where(j => j.Car == subtitle.Car).ToList();

                    if (matchingJavs.Count == 1)
                    {
                        // 如果只有一个匹配的 Jav，则将其 Subtitles 添加该字幕
                        matchingJavs[0].Subtitles.Add(subtitle);
                    }
                    else if (matchingJavs.Count > 1)
                    {
                        // 如果有多个匹配的 Jav，则给 subtitle 的 Error 属性赋值为 ScrapeError.重复文件
                        subtitle.Status = CrawlStatus.字幕无法确定归属;
                        notBelongedSubtitles.Add(subtitle!);
                    }
                    // 如果没有匹配的 Jav，则不做处理
                }
                // 如果找不到车牌，则不做处理
            }

            return notBelongedSubtitles;
        }

        private static bool TryCollectFc2Subtitle(string filePath, out Subtitle? subtitle)
        {
            if (CarUtils.TryExtractFc2Car(Path.GetFileNameWithoutExtension(filePath), out string? car))
            {
                subtitle = new Subtitle(filePath, car!);
                return true;
            }

            subtitle = null;
            return false;
        }

        private bool TryCollectCommonCarSubtitle(string filePath, out Subtitle? subtitle)
        {
            if (CarUtils.TryExtractCommonCar(Path.GetFileNameWithoutExtension(filePath),
                SettingsHolder.Standard.Birthmark.IgnoredWords, out string? car))
            {
                subtitle = new Subtitle(filePath, car!);
                return true;
            }

            subtitle = null;
            return false;
        }

        private bool TryCollectTerribleCarSubtitle(string filePath, out Subtitle? subtitle)
        {
            if (CarUtils.TryExtractTerribleCar(Path.GetFileNameWithoutExtension(filePath),
                SettingsHolder.Standard.Birthmark.IgnoredWords, out string? car))
            {
                subtitle = new Subtitle(filePath, car!);
                return true;
            }

            subtitle = null;
            return false;
        }

        /// <summary>
        /// 检查当前文件夹是否是某个车牌的独立文件夹
        /// </summary>
        /// <param name="subFolders"></param>
        /// <param name="javs"></param>
        /// <remarks>独立文件夹是指该文件夹仅用来存放一部车牌的影片，不包含“.actors”"extrafanrt”外的其他文件夹</remarks>
        private void IsCurrentFoldeSeparate(string[] subFolders, List<Jav> javs)
        {
            if (javs.Select(j => j.Car).Distinct().ToList().Count == 1
                && subFolders.All(folder => SettingsHolder.Standard.Birthmark.AttendantFolders.Contains(folder, StringComparer.OrdinalIgnoreCase)))
            {
                //（1）只有一种车牌，或者没有车牌（都是null），（2）且当前文件夹内没有其他无关文件夹
                javs.ForEach(jav => jav.IsSeparate = true);
            }
        }

        /// <summary>
        /// 更新同一车牌同一版本的jav的CD
        /// </summary>
        /// <param name="javs"></param>
        private static void GroupAndSortJavs(List<Jav> javs)
        {
            // 先按车牌和名称排序，后续按这个顺序入数据库
            javs = javs.OrderBy(j => j.Car)
                       .ThenBy(j => j.NameWithoutExt)
                       .ToList();

            // 先按车牌分组，再按版本二次分组
            var groupedJavs = javs.GroupBy(j => j.Car)
                                  //按CarName分组
                                  .Select(carGroup => new
                                  {
                                      CarName = carGroup.Key,
                                      Editions = carGroup.GroupBy(j => j.Edition)
                                                         //按Edition分组
                                                         .Select(editionGroup => new
                                                         {
                                                             Edition = editionGroup.Key,
                                                             CDs = editionGroup.ToList()
                                                         }).ToList()
                                  });

            // 给分组后的javs对象赋值
            foreach (var carGroup in groupedJavs)
            {
                int familyNo = 1;  //当前jav在同一车牌视频中的序号
                int familyCount = carGroup.Editions.Sum(editionGroup => editionGroup.CDs.Count);  //同一车牌的视频总数
                foreach (var editionGroup in carGroup.Editions)
                {
                    int cd = 1; //同一车牌同一版本下的第几个CD
                    int groupCount = editionGroup.CDs.Count;  //同一车牌同一版本下的CD总数
                    foreach (var jav in editionGroup.CDs)
                    {
                        jav.FamilyNo = familyNo++;
                        jav.FamilyCount = familyCount;
                        if (groupCount > 1)
                        {
                            // 如果同一车牌同一版本下有多个CD，给jav标记它是CD几
                            jav.CD = cd++;
                            jav.CDCount = groupCount;
                        }
                    }
                }
            }
        }

    }
}
