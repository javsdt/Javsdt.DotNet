using Javsdt.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Javsdt.Domain.Entitys
{
    public partial class Jav : BaseFile
    {
        // 供EF Core使用的无参构造方法
        public Jav() { }

        private Jav(string path) : base(path) { }

        /// <summary>
        /// 成功找到车牌
        /// </summary>
        /// <param name="path"></param>
        /// <param name="car"></param>
        public static Jav FoundCar(string path, string car) => new Jav(path)
        {
            Car = car,
            Status = CrawlStatus.成功
        };

        /// <summary>
        /// 找不到车牌
        /// </summary>
        /// <param name="path"></param>
        public static Jav NotFoundCar(string path) => new Jav(path)
        {
            Status = CrawlStatus.提取车牌失败
        };

        /// <summary>
        /// 车牌
        /// </summary>
        /// <example>ABC-123</example>
        public string? Car { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        public CrawlStatus Status { get; set; }

        /// <summary>
        /// 是否处于独立文件夹
        /// </summary>
        public bool IsSeparate { get; set; }

        /// <summary>
        /// 在当前车牌的家族中排序第几个
        /// </summary>
        /// <remarks>依据它判定最后一个jav处理完后，移动独立文件夹</remarks>
        public int FamilyNo { get; set; }

        /// <summary>
        /// 当前车牌的家族总共多少个
        /// </summary>
        /// <remarks>依据它判定最后一个jav处理完后，移动独立文件夹</remarks>
        public int FamilyCount { get; set; }

        /// <summary>
        /// 第几部分
        /// </summary>
        /// <remarks>
        /// 一部时间较长的影片可能被分为多部份，例如cd1 cd2 cd3的1 2 3；
        /// <para>不分CD则为0，在命名过程中隐身。</para>
        /// </remarks>
        public int CD { get; set; }

        /// <summary>
        /// 当前车牌总共多少部分
        /// </summary>
        /// <remarks>
        /// <para>例如abc-123分为abc-123-cd1.MP4和abc-123-cd2.mp4共两部分；</para>
        /// <para>不分CD则为0。</para>
        /// </remarks>
        public int CDCount { get; set; }

        /// <summary>
        /// 版本描述
        /// </summary>
        public string? Edition { get; set; }

        /// <summary>
        /// 是否有字幕
        /// </summary>
        /// <remarks>依据字幕文件是否存在，或者有-C等特征</remarks>
        public bool HasSubtitle { get; set; }

        /// <summary>
        /// 是否无码流出
        /// </summary>
        /// <example></example>
        public bool IsDivulged { get; set; }

        /// <summary>
        /// 是否AI破解
        /// </summary>
        /// <example></example>
        public bool IsCracked { get; set; }

        /// <summary>
        /// fanart路径
        /// </summary>
        public string? FanartPath { get; set; }

        #region 导航属性

        /// <summary>
        /// 完整字幕文件名
        /// </summary>
        /// <example>ABC-123-cd2</example>
        /// <remarks>影片和字幕需在同一文件夹内</remarks>
        public ICollection<Subtitle> Subtitles { get; set; } = [];

        #endregion

        /// <summary>
        /// 视频文件的版本
        /// <para>例如ABC-123 - ㊥ 4K.mp4，版本是【㊥ 4K】</para>
        /// </summary>
        [NotMapped]
        public string EditionCDn => $"{(Edition is null ? string.Empty : $" - {Edition}")}{(CD == 0 ? string.Empty : $"-cd{CD}")}";

    }
}
