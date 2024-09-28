using HappreeTool.Documents;
using Javsdt.Shared.Configuration;
using Javsdt.Application.Dtos;
using Javsdt.Application.Helpers.Base;
using Javsdt.Domain.Entitys;
using Javsdt.Domain.Exceptions;
using Javsdt.Domain.Services;
using Javsdt.Shared.Constants;
using Javsdt.Shared.Enums;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Xml.Serialization;

namespace Javsdt.Application.Helpers
{
    public class FileStandarder(ILogger<FileStandarder> _logger,
        JavService _javService, MovieService _movieService)
    {

        /// <summary>
        /// 路径：归类的目标根目录
        /// </summary>
        private string classifyRootDir = string.Empty;

        /// <summary>
        /// （新选择文件夹后）检查用户设置的“归类根目录”的合法性
        /// </summary>
        /// <param name="choosedDir"></param>
        /// <exception cref="ClasssifyRootDirOnAnotherDiskException"></exception>
        /// <exception cref="ClasssifyRootDirNotExistException"></exception>
        public void CheckCustomClassifyRoot(string choosedDir)
        {
            _logger.LogInformation("【检查归类根目录】begin");

            ClassifyOperationType operationType = SettingsHolder.Standard.Classify.ClassifyOperationType;
            if (operationType == ClassifyOperationType.NoOperation)
            {
                _logger.LogInformation("【检查归类根目录】end_用户不希望变动目录结构");
            }
            else if (operationType == ClassifyOperationType.ChooseDirCombineAlreadyClassify)
            {
                classifyRootDir = Path.Combine(choosedDir, ProcessConstant.ALREADY_CLASSIFY_DIR);
                _logger.LogInformation("【检查归类根目录】end_用户希望归类在【所选文件夹/归类完成：{classifyRootDir}】", classifyRootDir);
            }
            else if (operationType == ClassifyOperationType.OnlyChooseDir)
            {
                // 用户希望归类在“所选文件夹”，则归类到所选文件夹下的【归类完成】
                classifyRootDir = choosedDir;
                _logger.LogInformation("【检查归类根目录】end_用户希望就归类在【所选文件夹：{classifyDir}】", classifyRootDir);
            }
            else if (operationType == ClassifyOperationType.Custom)
            {
                classifyRootDir = SettingsHolder.Standard.Classify.ClassifyRootDir;
                _logger.LogInformation("【检查归类根目录】检查用户自定义的【{dir}】是否合理...", classifyRootDir);
                // 用户自定义了一个路径
                if (Path.GetPathRoot(classifyRootDir) != Path.GetPathRoot(choosedDir))
                    throw new ClasssifyRootDirOnAnotherDiskException(classifyRootDir);
                if (!Directory.Exists(classifyRootDir))
                    throw new ClasssifyRootDirNotExistException(classifyRootDir);
                _logger.LogInformation("【检查归类根目录】end_用户希望就归类在【自定义文件夹：{classifyDir}】", classifyRootDir);
            }
            else
            {
                throw new UnknownOperationException("【检查归类根目录】");
            }
        }

        /// <summary>
        /// 2重命名视频
        /// </summary>
        /// <param name="jav">jav视频文件对象</param>
        /// <remarks>重命名失败 => message提醒用户自行重命名 & 后续操作仍使用原文件名</remarks>
        public void RenameVideo(Jav jav, AssembleDto dto)
        {
            _logger.LogInformation("【重命名视频】begin_当前视频路径【{path}】", jav.AbsolutePath);

            // 新视频文件名，不带文件类型
            string newNameWithoutExt = $"{AssembleHelper.AssembleVideoFunction(dto)}{jav.EditionCDn}";
            // 视频文件的新路径
            string targetPath = Path.Combine(jav.Dir, $"{newNameWithoutExt}{jav.Ext}");
            _logger.LogInformation("【重命名视频】目标视频路径【{targetPath}】", targetPath);

            // 重命名视频文件
            try
            {
                FileUtils.MoveFile(jav.AbsolutePath, targetPath);
            }
            catch (Exception e)
            {
                throw new MoveFileWhenRenameException(jav.AbsolutePath, targetPath, e.Message);
            }

            // 更新
            jav.NameWithoutExt = newNameWithoutExt;
            _javService.UpdateNameWithoutExt(jav.Id, newNameWithoutExt);
            _logger.LogInformation("【重命名视频】end_最新视频路径【{currentPath}】", jav.AbsolutePath);
        }

        /// <summary>
        /// 针对文件夹归类
        /// </summary>
        /// <param name="jav"></param>
        /// <exception cref="NestingDollCreateSeparateFolderException"></exception>
        /// <exception cref="ClassifyDirAlreadyExistException"></exception>
        /// <exception cref="GetDirectoryException"></exception>
        /// <exception cref="MoveDirectoryWhenClassifyException"></exception>
        public void ClassifyDir(Jav jav, AssembleDto dto)
        {
            _logger.LogInformation("【归类目录】begin_当前视频路径【{path}】", jav.AbsolutePath);

            // 用户选择的文件夹是一部影片的独立文件夹（这次整理只整理了一部车牌），为了避免在这个文件夹里又创建新的归类文件夹（套娃）
            if (jav.IsSeparate && classifyRootDir.StartsWith(jav.Dir))
            {
                throw new NestingDollCreateSeparateFolderException(jav.Dir);
            }

            // 归类的目标文件夹路径
            string assemblePath = AssembleHelper.AssembleClassifyPathFunction(dto);  // 拼接的归类结构
            string targetDir = Path.Combine(classifyRootDir, assemblePath);  // 目标文件夹路径
            _logger.LogInformation("【归类目录】目标文件夹路径【{targetDir}】", targetDir);

            // 如果视频在独立文件夹中，且当前视频是家族中的最后一集，则移动文件夹
            if (jav.IsSeparate)
            {
                if (jav.FamilyNo == jav.FamilyCount)
                {
                    _logger.LogInformation("【归类目录】end_当前jav是家族中的【{FamilyNo}/{FamilyCount}】，准备搬迁文件夹",
                        jav.FamilyNo, jav.FamilyCount);
                    if (Path.Exists(targetDir))
                    {
                        throw new ClassifyDirAlreadyExistException(targetDir);
                    }
                    string parentDir = Path.GetDirectoryName(targetDir)
                        ?? throw new GetDirectoryException(targetDir);  // 目标文件夹路径的父目录
                    try
                    {
                        FileUtils.ConfirmDirExist(parentDir);
                        FileUtils.MoveDirectory(jav.Dir, targetDir);
                    }
                    catch (Exception e)
                    {
                        throw new MoveDirectoryWhenClassifyException(jav.Dir, targetDir, e.Message);
                    }
                }
                else
                {
                    // 当前视频不是家族中的最后一集，等最后一集移动
                    _logger.LogInformation("【归类目录】end_当前jav是家族中的【{FamilyNo}/{FamilyCount}】，暂不移动文件夹",
                        jav.FamilyNo, jav.FamilyCount);
                    return;
                }
            }
            else
            {
                // 当前视频不在独立文件夹中，不管视频是家族中的第几集，移动视频本身
                string targetPath = Path.Combine(targetDir, jav.NameWithExt);
                string parentDir = Path.GetDirectoryName(targetPath)
                    ?? throw new GetDirectoryException(targetPath);  // 目标文件夹路径的父目录
                _logger.LogInformation("【归类目录】end_当前jav不在独立文件夹中，准备归类至{targetPath}", targetPath);
                try
                {
                    FileUtils.ConfirmDirExist(parentDir);
                    FileUtils.MoveFile(jav.AbsolutePath, targetPath);
                }
                catch (Exception e)
                {
                    throw new MoveFileWhenClassifyException(jav.AbsolutePath, targetPath, e.Message);
                }
            }

            jav.Dir = targetDir;
            _javService.UpdateDirectory(jav.Id, targetDir);
            _logger.LogInformation("【归类目录】end_最新视频路径【{currentPath}】", jav.AbsolutePath);
        }

        /// <summary>
        /// 3重命名字幕
        /// </summary>
        /// <param name="javFile"></param>
        /// <remarks>重命名失败 => message提醒用户自行重命名 & 后续操作仍使用原文件名</remarks>
        public void RenameSubtitle(Jav javFile, AssembleDto dto)
        {
            //_logger.LogInformation("【重命名字幕】begin");
            //foreach (Subtitle subtitle in javFile.Subtitles)
            //{
            //    // 新字幕文件名
            //    string newSubtitle = $"{javFile.NameWithoutExt}{javFile.SubtitleExt}";
            //    // 字幕文件的新路径
            //    string newSubtitlePath = Path.Combine(javFile.Dir, newSubtitle);
            //    // 重命名视频文件
            //    if (!FileUtils.TryMoveFile(javFile.SubtitlePath, newSubtitlePath, out message))
            //    {
            //        return false;
            //    }

            //    javFile.Subtitle = newSubtitle; // 更新
            //}
        }

        /// <summary>
        /// 4归类影片的视频文件
        /// </summary>
        /// <param name="jav"></param>
        /// <remarks>不处理所在文件夹</remarks>
        public void ClassifyVideo(Jav jav, AssembleDto dto)
        {
            _logger.LogInformation("【归类视频】begin");

            //归类的目标文件夹路径
            string assemblePath = AssembleHelper.AssembleClassifyPathFunction(dto);
            string targetDir = Path.Combine(classifyRootDir, assemblePath);
            _logger.LogInformation("【归类视频】目标文件夹路径【{targetDir}】", targetDir);
            if (Path.Exists(targetDir))
            {
                _logger.LogWarning("针对视频文件归类时，目标文件夹【{targetDir}】已存在", targetDir);
            }

            //归类的目标视频文件路径
            string targetPath = Path.Combine(targetDir, jav.NameWithExt);
            _logger.LogInformation("【归类视频】目标视频路径【{targetPath}】", targetPath);
            if (Path.Exists(targetDir))
            {
                throw new ClassifyVideoAlreadyExistException(targetPath);
            }

            // 移动视频文件
            try
            {
                FileUtils.ConfirmDirExist(targetDir);
                FileUtils.MoveFile(jav.AbsolutePath, targetPath);
            }
            catch (Exception ex)
            {
                throw new MoveFileWhenClassifyException(jav.AbsolutePath, targetPath, ex.Message);
            }

            // 更新
            jav.Dir = targetDir;
            _javService.UpdateDirectory(jav.Id, targetDir);
            _logger.LogInformation("【归类视频】end_最新视频路径【{path}】", jav.AbsolutePath);
        }

        /// <summary>
        /// 5归类影片的字幕文件
        /// </summary>
        /// <param name="jav"></param>
        /// <remarks>不处理所在文件夹</remarks>
        public void ClassifySubtitle(Jav jav, AssembleDto dto)
        {
            _logger.LogInformation("【归类字幕】begin");

            //新的字幕文件路径
            //string targetPath = Path.Combine(jav.Dir, jav.Subtitle);
            //移动字幕文件
            //if (!FileUtils.TryMoveFile(jav.AbsolutePath, targetPath, out message))
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// 7写nfo
        /// </summary>
        public void WriteNfo(Jav jav, Movie movie, AssembleDto dto)
        {
            _logger.LogInformation("【保存nfo】begin");

            // nfo路径，如果是为kodi准备的nfo，不需要多cd
            string nfoFileName = SettingsHolder.Standard.Kodi.OnlyOneWhenCDs
                ? $"{jav.NameWithoutExt.Replace(jav.EditionCDn, "")}.nfo"
                : $"{jav.NameWithoutExt}.nfo";
            string pathNfo = Path.Combine(jav.Dir, nfoFileName);
            _logger.LogInformation("【保存nfo】目标nfo路径【{path}】", pathNfo);

            MovieNfo movieNfo = new MovieNfo(movie, dto);
            XmlSerializer serializer = new XmlSerializer(typeof(MovieNfo));
            using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, movieNfo);
                string xmlContent = textWriter.ToString();

                // 打印生成的 XML 内容
                _logger.LogInformation("【保存nfo】nfo内容为:\n{xmlContent}", xmlContent);

                // 将 XML 内容写入文件
                using (StreamWriter fileWriter = new StreamWriter(pathNfo, false, Encoding.UTF8))
                {
                    fileWriter.Write(xmlContent);
                }
            }

            _logger.LogInformation("【保存nfo】end");
        }

        /// <summary>
        /// 8下载fanart
        /// </summary>
        /// <param name="jav"></param>
        public void DownloadFanart(Jav jav, AssembleDto dto)
        {
            _logger.LogInformation("【下载fanart】begin");

            string aseembleFanart = AssembleHelper.AssembleFanartFunction(dto);
            string fanartPath = Path.Combine(jav.Dir, aseembleFanart);
            _movieService.DownloadFanart(jav.Car!, fanartPath);

            _logger.LogInformation("【下载fanart】end");
        }

        /// <summary>
        /// 8下载poster
        /// </summary>
        /// <param name="jav"></param>
        public void DownloadPoster(Jav jav, AssembleDto dto)
        {
            _logger.LogInformation("【下载poster】begin");

            string aseemblePoster = AssembleHelper.AssemblePosterFunction(dto);
            string posterPath = Path.Combine(jav.Dir, aseemblePoster);
            _movieService.DownloadPoster(jav.Car!, posterPath);

            _logger.LogInformation("【下载poster】end");
        }

        /// <summary>
        /// 10收集头像
        /// </summary>
        public void CollectSculpture()
        {
            _logger.LogInformation("【收集头像】begin");
        }

    }
}