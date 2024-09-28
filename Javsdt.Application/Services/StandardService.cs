using Javsdt.Shared.Configuration;
using Javsdt.Application.Dtos;
using Javsdt.Application.Helper;
using Javsdt.Application.Helpers;
using Javsdt.Domain.Entitys;
using Javsdt.Domain.Exceptions;
using Javsdt.Domain.Services;
using Javsdt.Shared.Enums;
using Microsoft.Extensions.Logging;

namespace Javsdt.Application.Services
{
    public class StandardService(ILogger<StandardService> _logger, 
        FileStandarder _javHelper, FileExplorer _fileExplorer,
        JavService _javService, MovieService _movieService)
    {

        /// <summary>
        /// 设定每批处理的数据量大小
        /// </summary>
        private static readonly int batchSize = 100;

        public void Do(string chooseDir)
        {
            CheckBeforeTask(chooseDir);

            //清空先前的整理任务
            _javService.Clear();

            //快速检索所有jav对象入库
            _fileExplorer.CollectJavFilesInRootDir(chooseDir);

            HandleJavs();
        }

        private void CheckBeforeTask(string chooseDir)
        {
            _javHelper.CheckCustomClassifyRoot(chooseDir);
        }

        private void HandleJavs()
        {
            //取出未整理完成jav对象
            int total = _javService.GetTotalCount(); // 获取总数据量
            int pageNo = 0;  //第几页

            while (true)
            {
                List<Jav> batchJavs = _javService.GetPagedResultsAsync(pageNo, batchSize);
                if (batchJavs.Count == 0)
                {
                    break;
                }

                foreach (Jav jav in batchJavs)
                {
                    int no = batchSize * pageNo + batchJavs.IndexOf(jav) + 1;
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
                        Movie? movie = _movieService.GetDetail(jav.Car!)!;
                        if (movie == null)
                        {
                            _logger.LogWarning("当前车牌可能不正确或者正在等待MovieDb刮削");
                            continue;
                        }

                        AssembleDto dto = new AssembleDto(jav, movie);
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

        private void OrganizeFileAndFolder(Jav jav, Movie movie, AssembleDto dto)
        {
            //1 重命名视频
            if (SettingsHolder.Standard.Video.NeedRename)
            {
                _javHelper.RenameVideo(jav, dto);
            }
            if (SettingsHolder.Standard.Subtitle.NeedRename)
            {
                _javHelper.RenameSubtitle(jav, dto);
            }

            //2 针对文件归类影片
            if (SettingsHolder.Standard.Classify.ClassifyOperationType != ClassifyOperationType.NoOperation)
            {
                if (SettingsHolder.NeedSeparateFolder)
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
            if (SettingsHolder.Standard.Nfo.Need)
            {
                _javHelper.WriteNfo(jav, movie, dto);
            }

            //5 下载Fanart
            if (SettingsHolder.Standard.Fanart.Need)
            {
                _javHelper.DownloadFanart(jav, dto);
            }

            //6 下载Poster
            if (SettingsHolder.Standard.Poster.Need)
            {
                _javHelper.DownloadPoster(jav, dto);
            }

            //7 收集演员头像

        }

    }
}