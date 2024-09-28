using HappreeTool.CommonUtils;
using Javsdt.Shared.Configuration;
using Javsdt.Application.Dtos;

namespace Javsdt.Application.Helpers.Base
{
    public class AssembleHelper()
    {
        public static Func<AssembleDto, string> AssembleVideoFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Standard.Video.NameVideoFormula);
        public static Func<AssembleDto, string> AssembleClassifyPathFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Standard.Classify.ClassifyRelativePathFormula);
        public static Func<AssembleDto, string> AssembleNfoTitleFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Standard.Nfo.NfoTitleFormula);
        public static Func<AssembleDto, string> AssembleFanartFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Standard.Fanart.NameFanartFormula);
        public static Func<AssembleDto, string> AssemblePosterFunction 
            => AssembleUtils<AssembleDto>.CompileConcatHandler(SettingsHolder.Standard.Poster.NamePosterFormula);
        public static Func<AssembleDto, IEnumerable<string>> CollectPropertiesFunction 
            => AssembleUtils<AssembleDto>.CompilePropertiesHandler(SettingsHolder.Standard.Nfo.ExtraCollectProperties);
    }
}
