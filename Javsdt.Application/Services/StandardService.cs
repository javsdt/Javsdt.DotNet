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
    public class StandardService(
        ILogger<StandardService> _logger,
        FileStandarder _javHelper,
        FileExplorer _fileExplorer,
        JavService _javService,
        MovieService _movieService,
        IOptions<StandardSettings> options)
    {
        /// <summary>
        /// 设定每批处理的数据量大小
        /// </summary>
        private static readonly int batchSize = 100;

        public void Do(string chooseDir)
        {
            // 检查一些预设项
            CheckBeforeTask(chooseDir);

            // 清空先前的整理任务
            _javService.Clear();

            // 快速检索所有jav对象入库
            _fileExplorer.CollectJavFilesInRootDir(chooseDir);

            // 处理所有jav
            HandleJavs();
        }

        /// <summary>
        /// 任务前检查
        /// </summary>
        /// <param name="chooseDir"></param>
        private void CheckBeforeTask(string chooseDir)
        {
            _javHelper.CheckCustomClassifyRoot(chooseDir);
        }

        /// <summary>
        /// 处理所有
        /// </summary>
        private void HandleJavs()
        {
            // 分页
            int total = _javService.CountAllToHandle(); // 获取总数据量
            int pageNo = 0; //第几页

            while (true)
            {
                // 所有待处理的jav
                List<Jav> batchJavs = _javService.GetPagedResultsAsync(pageNo, batchSize);
                if (batchJavs.Count == 0)
                {
                    _logger.LogInformation("没有待处理的jav！");
                    break;
                }

                foreach (Jav jav in batchJavs)
                {
                    // 当前在批次中的序号
                    int no = batchSize * pageNo + batchJavs.IndexOf(jav) + 1;

                    // 当前jav可能因为某原因无法处理
                    if (jav.Status != CrawlStatus.成功)
                    {
                        _logger.LogWarning("【单jav处理】无法处理【{no}/{total}】，原因:【{reason}】，【{currentPath}】...",
                            no, total, jav.Status.ToString(), jav.OriginAbsolutePath);
                        continue;
                    }

                    _logger.LogInformation("【单jav处理】begin_开始处理【{no}/{total}】【{currentPath}】...",
                        no, total, jav.OriginAbsolutePath);

                    try
                    {
                        List<Movie> movies = _movieService.GetDetail(jav.Car!)!;
                        if (movies.Count == 0)
                        {
                            _logger.LogWarning("当前车牌可能不正确或者正在等待MovieDb刮削");
                            continue;
                        }

                        if (movies.Count > 1)
                        {
                            _logger.LogWarning("当前车牌对应的影片不唯一，请用指定id的方式刮削");
                            continue;
                        }

                        Movie movie = movies[0];
                        AssembleDto dto = new AssembleDto(jav, movie, options.Value);
                        OrganizeFileAndFolder(jav, movie, dto);
                    }
                    catch (NonFatalException ex)
                    {
                        _logger.LogError("【单jav处理】end_处理失败【{no}/{total}】【{currentPath}】, 原因：{message}",
                            no, total, jav.AbsolutePath, ex.Message);
                        continue;
                    }

                    _logger.LogInformation("【单jav处理】end_处理成功【{no}/{total}】【{currentPath}】", no, total, jav.AbsolutePath);
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
        private void OrganizeFileAndFolder(Jav jav, Movie movie, AssembleDto dto)
        {
            //1 重命名视频
            if (是否重命名视频)
            {
                _javHelper.RenameVideo(jav, dto);
            }

            if (是否重命名字幕)
            {
                _javHelper.RenameSubtitle(jav, dto);
            }

            //2 针对文件归类影片
            if (归类方式 != ClassifyOperationType.NoOperation)
            {
                if (是否需要独立文件夹)
                {
                    _javHelper.ClassifyDir(jav, dto);
                }
                else
                {
                    _javHelper.ClassifyVideo(jav, dto);
                    _javHelper.ClassifySubtitle(jav, dto);
                }
            }

            // 4 写nfo
            if (是否需要nfo)
            {
                _javHelper.WriteNfo(jav, movie, dto);
            }

            //5 下载Fanart
            if (是否需要fanart)
            {
                _javHelper.DownloadFanart(jav, dto);
            }

            //6 下载Poster
            if (是否需要poster)
            {
                _javHelper.DownloadPoster(jav, dto);
            }

            //7 收集演员头像
        }

        private readonly bool 是否重命名视频 = options.Value.视频.是否重命名视频;
        private readonly bool 是否重命名字幕 = options.Value.字幕.是否重命名字幕;
        private readonly ClassifyOperationType 归类方式 = options.Value.归类.归类方式;
        private readonly bool 是否需要独立文件夹 = options.Value.归类.JudgeNeedSeparateFolder();
        private readonly bool 是否需要nfo = options.Value.Nfo.是否需要;
        private readonly bool 是否需要fanart = options.Value.Fanart.是否需要;
        private readonly bool 是否需要poster = options.Value.Poster.是否需要;
    }
}