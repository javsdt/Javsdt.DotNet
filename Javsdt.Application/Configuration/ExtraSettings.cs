using Javsdt.Shared.Converters;
using Javsdt.Shared.Enums;
using System.Text.Json.Serialization;

namespace Javsdt.Application.Configuration
{
    public class StandardSettings
    {
        public BirthmarkSettings Birthmark { get; set; } = new BirthmarkSettings();
        public ElementSettings Element { get; set; } = new ElementSettings();
        public VideoSettings Video { get; set; } = new VideoSettings();
        public SubtitleSettings Subtitle { get; set; } = new SubtitleSettings();
        public ClassifySettings Classify { get; set; } = new ClassifySettings();
        public NfoSettings Nfo { get; set; } = new NfoSettings();
        public FanartSettings Fanart { get; set; } = new FanartSettings();
        public PosterSettings Poster { get; set; } = new PosterSettings();
        public EmbySettings Emby { get; set; } = new EmbySettings();
        public KodiSettings Kodi { get; set; } = new KodiSettings();

    }

    public class BirthmarkSettings
    {
        public List<string> IgnoredWords { get; set; } = new List<string>();
        public List<string> SubtitleWords { get; set; } = new List<string>();
        public List<string> DivulgedWords { get; set; } = new List<string>();
        public List<string> CrackedWords { get; set; } = new List<string>();
        public List<string> InterfereSubtitleWords { get; set; } = new List<string>();
        public List<string> InterfereDivulgedWords { get; set; } = new List<string>();
        public List<string> InterfereCrackedWords { get; set; } = new List<string>();
        public List<string> AttendantFolders { get; set; } = new List<string>();
        public List<string> ExcludeFolders { get; set; } = new List<string>();
    }

    public class ElementSettings
    {
        public bool ReserveTitleEndActors { get; set; }
        public int TitleLimit { get; set; }
        public string SubtitleStamp { get; set; } = string.Empty;
        public string DivulgedStamp { get; set; } = string.Empty;
        public string CrackedStamp { get; set; } = string.Empty;
        public string DatePattern { get; set; } = "yyyy-MM-dd";
    }

    public class VideoSettings
    {
        /// <summary>
        /// 是否重命名视频
        /// </summary>
        public bool NeedRename { get; set; }
        public List<string> NameVideoFormula { get; set; } = new List<string>();
        public List<string> VideoTypes { get; set; } = new List<string>();
    }

    public class SubtitleSettings
    {
        /// <summary>
        /// 是否重命名用户已拥有的字幕
        /// </summary>
        public bool NeedRename { get; set; }
        public bool SkipCollectSubtitleIfExist { get; set; }
    }

    public class ClassifySettings
    {
        public string ClassifyRootDir { get; set; } = string.Empty;

        [JsonConverter(typeof(ClassifyOperationTypeConverter))]
        public ClassifyOperationType ClassifyOperationType { get; set; }

        public List<string> ClassifyRelativePathFormula { get; set; } = new List<string>();
    }

    public class NfoSettings
    {
        /// <summary>
        /// 是否收集nfo
        /// </summary>
        public bool Need { get; set; }
        public bool NeedZhPlot { get; set; }
        public bool NeedJavLibraryReview { get; set; }
        public List<string> NfoTitleFormula { get; set; } = new List<string>();
        public List<string> ExtraCollectProperties { get; set; } = new List<string>();
    }

    public class FanartSettings
    {
        public bool Need { get; set; }
        public List<string> NameFanartFormula { get; set; } = new List<string>();
    }

    public class PosterSettings
    {
        public bool Need { get; set; }
        public List<string> NamePosterFormula { get; set; } = new List<string>();
        public bool NeedSubtitleWatermark { get; set; }
        public bool NeedDivulgeWatermark { get; set; }
        public bool NeedLocateFacial { get; set; }
    }

    public class EmbySettings
    {
        public string Server { get; set; } = string.Empty;
        public string ApiId { get; set; } = string.Empty;
        public bool OverwritePreviousHeadSculpture { get; set; }
    }

    public class KodiSettings
    {
        public bool OnlyOneWhenCDs { get; set; }
        public bool NeedHeadSculpture { get; set; }
    }
}
