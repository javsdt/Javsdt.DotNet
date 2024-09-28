using Javsdt.Shared.Enums;

namespace Javsdt.Domain.Entitys
{
    public class Subtitle : BaseFile
    {
        // 供EF Core使用的无参构造方法
        public Subtitle() { }

        private Subtitle(string path) : base(path) { }


        /// <summary>
        /// 找到车牌的构造函数
        /// </summary>
        /// <param name="path"></param>
        /// <param name="carName"></param>
        public Subtitle(string path, string carName) : this(path)
        {
            Car = carName;
        }

        /// <summary>
        /// 找不到车牌的构造函数
        /// </summary>
        /// <param name="path"></param>
        public static Subtitle NotFoundCar(string path) => new Subtitle(path)
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
        /// 归属于视频文件
        /// </summary>
        public ICollection<Jav> Javs { get; set; } = [];

    }
}
