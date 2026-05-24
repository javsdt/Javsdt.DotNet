using HappreeTool.Utils.CommonUtils;
using Javsdt.Domain.Configuration;
using Javsdt.Domain.Dtos;

namespace Javsdt.Domain.Helpers.Base
{
    public class AssembleHelper()
    {
        public static Func<AssembleDto, string> AssembleVideoFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.重命名视频文件的公式);
        public static Func<AssembleDto, string> AssembleClassifyPathFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.归类的标准);
        public static Func<AssembleDto, string> AssembleNfoTitleFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Nfo的title的公式);
        public static Func<AssembleDto, string> AssembleFanartFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Fanart的公式);
        public static Func<AssembleDto, string> AssemblePosterFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Poster的公式);
        public static Func<AssembleDto, IEnumerable<string>> CollectPropertiesFunction 
            => AssembleUtils<AssembleDto>.CompilePropertiesHandler(SettingsHolder.额外增加以下元素到特征中);
    }
}
