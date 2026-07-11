using Javsdt.Domain.Dtos;
using Javsdt.Domain.Entitys;
using Javsdt.Domain.Exceptions;
using Javsdt.Domain.Helpers;
using Javsdt.Domain.Services;
using Javsdt.Domain.Configuration;
using Javsdt.Shared.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Javsdt.Application.Services
{
    public class StandardService(ILogger<StandardService> logger,
                                 FileStandarder javHelper,
                                 FileExplorer fileExplorer,
                                 JavService javService,
                                 MovieService movieService,
                                 IOptions<StandardSettings> options)
    {
        /// <summary>
        /// 设定每批处理的数据量大小
        /// </summary>
        private const int batchSize = 100;

        public async Task Do(string chooseDir)
        {
            // 检查一些预设项
            CheckBeforeTask(chooseDir);

            // 清空先前的整理任务
            javService.Clear();

            // 快速检索所有jav对象入库
            await fileExplorer.CollectJavFilesInRootDir(chooseDir);

            // 处理所有jav
            await HandleJavs();
        }

        /// <summary>
        /// 任务前检查
        /// </summary>
        /// <param name="chooseDir"></param>
        private void CheckBeforeTask(string chooseDir)
        {
            javHelper.CheckCustomClassifyRoot(chooseDir);
        }

        /// <summary>
        /// 处理所有
        /// </summary>
        private async Task HandleJavs()
        {
            // 分页
            int total = javService.CountAllToHandle(); // 获取总数据量
            int pageNo = 0; //第几页

            while (true)
            {
                // 所有待处理的jav
                List<Jav> batchJavs = javService.GetPagedResultsAsync(pageNo, batchSize);
                if (batchJavs.Count == 0)
                {
                    logger.LogInformation("没有待处理的jav！");
                    break;
                }

                foreach (Jav jav in batchJavs)
                {
                    // 当前在批次中的序号
                    int no = batchSize * pageNo + batchJavs.IndexOf(jav) + 1;

                    // 当前jav可能因为某原因无法处理
                    if (jav.Status != CrawlStatus.成功)
                    {
                        logger.LogError("【单jav处理】无法处理【{no}/{total}】，原因:【{reason}】，【{currentPath}】...",
                            no, total, jav.Status.ToString(), jav.OriginAbsolutePath);
                        continue;
                    }

                    logger.LogInformation(
                        "【单jav处理】begin_开始处理【{no}/{total}】【{currentPath}】，cd【{cd}/{cdCount}】，family【{fn}/{fc}】...",
                        no, total, jav.OriginAbsolutePath, jav.CD, jav.CDCount, jav.FamilyNo, jav.FamilyCount);

                    try
                    {
                        List<Movie> movies = await movieService.GetDetail(jav.Car!);
                        if (movies.Count == 0)
                        {
                            logger.LogWarning("当前车牌【{code}】可能不正确或者正在等待MovieDb刮削", jav.Car);
                            continue;
                        }

                        if (movies.Count > 1)
                        {
                            logger.LogWarning("当前车牌【{code}】对应的影片不唯一，请用指定id的方式刮削", jav.Car);
                            continue;
                        }

                        Movie movie = movies[0];
                        // if (movie.Type == JavType.无码)
                        // {
                        //     logger.LogWarning("跳过无码影片");
                        //     continue;
                        // }

                        AssembleDto dto = new AssembleDto(jav, movie, options.Value);
                        await OrganizeFileAndFolder(jav, movie, dto);
                    }
                    catch (NonFatalException ex)
                    {
                        logger.LogError("【单jav处理】end_处理失败【{no}/{total}】【{currentPath}】, 原因：{message}",
                            no, total, jav.AbsolutePath, ex.Message);
                        continue;
                    }

                    logger.LogInformation("【单jav处理】end_处理成功【{no}/{total}】【{currentPath}】", no, total,
                        jav.AbsolutePath);
                }

                pageNo += 1;
            }
        }

        /// <summary>
        /// 修改jav文件、写入nfo等
        /// </summary>
        /// <param name="jav"></param>
        /// <param name="movie"></param>
        /// <param name="dto"></param>
        private async Task OrganizeFileAndFolder(Jav jav, Movie movie, AssembleDto dto)
        {
            //1 重命名视频
            if (_是否重命名视频)
            {
                javHelper.RenameVideo(jav, dto);
            }

            if (_是否重命名字幕)
            {
                javHelper.RenameSubtitle(jav, dto);
            }

            //2 针对文件归类影片
            if (_归类方式 != ClassifyOperationType.NoOperation)
            {
                if (_是否需要独立文件夹)
                {
                    javHelper.ClassifyDir(jav, dto);
                }
                else
                {
                    javHelper.ClassifyVideo(jav, dto);
                    javHelper.ClassifySubtitle(jav, dto);
                }
            }

            // 4 写nfo
            if (_是否需要nfo)
            {
                javHelper.WriteNfo(jav, movie, dto);
            }

            //5 下载Fanart
            if (_是否需要fanart)
            {
                await javHelper.DownloadFanart(jav, dto);
            }

            //6 下载Poster
            if (_是否需要poster)
            {
                await javHelper.DownloadPoster(jav, dto);
            }

            //7 收集演员头像
        }

        private readonly bool _是否重命名视频 = options.Value.视频.是否重命名视频;
        private readonly bool _是否重命名字幕 = options.Value.字幕.是否重命名字幕;
        private readonly ClassifyOperationType _归类方式 = options.Value.归类.归类方式;
        private readonly bool _是否需要独立文件夹 = options.Value.归类.JudgeNeedSeparateFolder();
        private readonly bool _是否需要nfo = options.Value.Nfo.是否需要;
        private readonly bool _是否需要fanart = options.Value.Fanart.是否需要fanart;
        private readonly bool _是否需要poster = options.Value.Fanart.是否需要poster;
    }
}