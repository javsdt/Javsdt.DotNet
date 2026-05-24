using HappreeTool.Configurations;
using Microsoft.Extensions.Configuration;

namespace Javsdt.Domain.Configuration
{
    public static class SettingsHolder
    {
        /// <summary>
        /// 是否每个车牌单独一个文件夹
        /// </summary>
        public static bool NeedSeparateFolder { get; private set; }

        /// <summary>
        /// 重命名视频文件的公式 NameVideoFormula
        /// </summary>
        public static List<string> 重命名视频文件的公式 { get; set; } = [];

        /// <summary>
        /// 归类的标准 ClassifyRelativePathFormula
        /// </summary>
        public static List<string> 归类的标准 { get; set; } = new List<string>();

        /// <summary>
        /// nfo的title的公式 NfoTitleFormula
        /// </summary>
        public static List<string> Nfo的title的公式 { get; set; } = new List<string>();

        /// <summary>
        /// fanart的公式 NameFanartFormula
        /// </summary>
        public static List<string> Fanart的公式 { get; set; } = new List<string>();
        
        /// <summary>
        /// poster的公式 NamePosterFormula
        /// </summary>
        public static List<string> Poster的公式 { get; set; } = new List<string>();
        
        /// <summary>
        /// 额外增加以下元素到特征中 ExtraCollectProperties
        /// </summary>
        public static List<string> 额外增加以下元素到特征中 { get; set; } = new List<string>();

        public static void InitializeStandard()
        {
            IConfiguration configuration = ConfigurationLoader.LoadModuleConfiguration(
                AppContext.BaseDirectory, nameof(Domain));
            StandardSettings settings = configuration.GetSection("Standard").Get<StandardSettings>()!;

            重命名视频文件的公式 = settings.视频.重命名视频文件的公式;
            归类的标准 = settings.归类.归类的标准;
            Nfo的title的公式 = settings.Nfo.Title的公式;
            Fanart的公式 = settings.Fanart.Fanart的公式;
            Poster的公式 = settings.Fanart.Poster的公式;
            额外增加以下元素到特征中 = settings.Nfo.额外增加以下元素到特征中;
            NeedSeparateFolder = JudgeNeedSeparateFolder();
        }

        /// <summary>
        /// 是否需要独立文件夹
        /// </summary>
        /// <returns></returns>
        private static bool JudgeNeedSeparateFolder()
        {
            int index = 归类的标准.LastIndexOf("\\");
            return 归类的标准[(index + 1)..].Contains("Car");
        }
    }
}