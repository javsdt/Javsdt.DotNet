using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Javsdt.Domain.Entitys
{
    public class BaseEntity
    {

        [Description("创建时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? CreateTime { get; set; } = DateTime.Now;


        [Description("修改时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime? ModifyTime { get; set; } = DateTime.Now;


        [Description("版本")]
        public int Version { get; set; }

    }
}
