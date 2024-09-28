using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Javsdt.Domain.Exceptions;

namespace Javsdt.Domain.Entitys
{
    public class BaseFile : BaseEntity
    {
        public BaseFile(string path)
        {
            Ext = Path.GetExtension(path).ToLower();
            NameWithoutExt = OriginNameWithoutExt = Path.GetFileNameWithoutExtension(path);
            Dir = OriginDir = Path.GetDirectoryName(path) ?? throw new GetDirectoryException(path);
        }

        public BaseFile() : this(Assembly.GetEntryAssembly()!.Location) { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 原文件夹绝对路径
        /// </summary>
        public string OriginDir { get; set; }

        /// <summary>
        /// 原基本名
        /// </summary>
        /// <remarks>组装元素</remarks>
        public string OriginNameWithoutExt { get; private set; }

        /// <summary>
        /// 当前文件夹绝对路径
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 当前文件名不带扩展名
        /// </summary>
        public string NameWithoutExt { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        /// <example>.mp4</example>
        public string Ext { get; set; }

        /// <summary>
        /// 原绝对路径
        /// </summary>
        /// <remarks>记录初始状态，方便还原</remarks>
        [NotMapped]
        public string OriginAbsolutePath => Path.Combine(OriginDir, $"{OriginNameWithoutExt}{Ext}");

        [NotMapped]
        public string AbsolutePath => Path.Combine(Dir, $"{NameWithoutExt}{Ext}");

        [NotMapped]
        public string NameWithExt => $"{NameWithoutExt}{Ext}";

    }
}
