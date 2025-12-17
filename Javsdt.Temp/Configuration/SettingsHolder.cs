using Microsoft.Extensions.Configuration;

namespace Javsdt.Shared.Configuration
{
    public static class SettingsHolder
    {
        public static StandardSettings Standard { get; private set; } = new StandardSettings();

        /// <summary>
        /// 是否每个车牌单独一个文件夹
        /// </summary>
        public static bool NeedSeparateFolder { get; private set; }

        public static void InitializeStandard(IConfiguration configuration)
        {
            configuration.GetSection(nameof(Standard)).Bind(Standard);
            NeedSeparateFolder = JudgeNeedSeparateFolder();
        }

        private static bool JudgeNeedSeparateFolder()
        {
            List<string> paths = Standard.Classify.ClassifyRelativePathFormula;
            int index = paths.LastIndexOf("\\");
            return paths[(index + 1)..].Contains("Car");
        }
    }
}
