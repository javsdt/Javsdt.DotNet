namespace Javsdt.Shared.Enums
{
    public enum CrawlStatus
    {
        未知错误 = 0,
        成功 = 1,
        提取车牌失败 = 2,
        暂不支持的车牌 = 3,
        重复文件 = 4,
        字幕无法确定归属 = 5,
        同一影片分布在不同文件夹 = 6,
    }

    /// <summary>
    /// 选择哪一种方式规范目录结构
    /// </summary>
    public enum ClassifyOperationType
    {
        /// <summary>
        /// 无需操作，用户想保存在原父文件夹
        /// </summary>
        NoOperation = 0,

        /// <summary>
        /// 所选文件夹加【归类完成文件夹】
        /// </summary>
        ChooseDirCombineAlreadyClassify = 1,

        /// <summary>
        /// 所选文件夹加【归类完成文件夹】
        /// </summary>
        OnlyChooseDir = 2,

        /// <summary>
        /// 自定义归类根目录
        /// </summary>
        Custom = 3
    }

    /// <summary>
    /// 刮削完成度
    /// </summary>
    /// <remarks>在哪几个网站拿到了数据</remarks>
    public enum ScrapeModeEnum
    {
        /// <summary>
        /// 搜索
        /// </summary>
        Search = 1,

        /// <summary>
        /// 用户指定网址
        /// </summary>
        Appoint = 2,

        /// <summary>
        /// 用户屏蔽网站
        /// </summary>
        Skip = 3,

        /// <summary>
        /// 用户指定网址但错误
        /// </summary>
        Appoint_Error = 4,
    }

    public enum OtherService
    {
        Mine = 0,
    }

}
