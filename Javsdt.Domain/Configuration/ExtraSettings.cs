using Javsdt.Shared.Converters;
using Javsdt.Shared.Enums;
using System.Text.Json.Serialization;
using Javsdt.Domain.Dtos;

namespace Javsdt.Domain.Configuration
{
    public class StandardSettings
    {
        public BirthmarkSettings Birthmark { get; set; } = new BirthmarkSettings();
        public ElementSettings Element { get; set; } = new ElementSettings();
        public VideoSettings 视频 { get; set; } = new VideoSettings();
        public SubtitleSettings 字幕 { get; set; } = new SubtitleSettings();
        public ClassifySettings 归类 { get; set; } = new ClassifySettings();
        public NfoSettings Nfo { get; set; } = new NfoSettings();
        public FanartSettings Fanart { get; set; } = new FanartSettings();
        public EmbySettings Emby { get; set; } = new EmbySettings();
        public KodiSettings Kodi { get; set; } = new KodiSettings();
    }

    /// <summary>
    /// 原影片文件的性质 BirthmarkSettings
    /// </summary>
    public class BirthmarkSettings
    {
        /// <summary>
        /// 无视多余的字母数字 IgnoredWords
        /// </summary>
        public List<string> 无视多余的字母数字 { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public List<string> 是否中字即文件名包含 { get; set; } = new List<string>();

        /// <summary>
        /// 是否流出即文件名包含 DivulgedWords
        /// </summary>
        public List<string> 是否流出即文件名包含 { get; set; } = new List<string>();

        /// <summary>
        /// 是否破解即文件名包含 CrackedWords
        /// </summary>
        public List<string> 是否破解即文件名包含 { get; set; } = new List<string>();

        /// <summary>
        /// 中字干扰项 InterfereSubtitleWords
        /// </summary>
        public List<string> 中字干扰项 { get; set; } = new List<string>();

        /// <summary>
        /// 流出干扰项 InterfereDivulgedWords
        /// </summary>
        public List<string> 流出干扰项 { get; set; } = new List<string>();

        /// <summary>
        /// 破解干扰项 InterfereCrackedWords
        /// </summary>
        public List<string> 破解干扰项 { get; set; } = new List<string>();

        /// <summary>
        /// 随从文件夹们 AttendantFolders
        /// </summary>
        public List<string> 随从文件夹们 { get; set; } = new List<string>();

        public List<string> 排除文件夹们 { get; set; } = new List<string>();
    }

    public class ElementSettings
    {
        public bool ReserveTitleEndActors { get; set; }

        /// <summary>
        /// 标题长度限制 TitleLimit
        /// </summary>
        public int 标题长度限制 { get; set; }

        /// <summary>
        /// 是否中字的表现形式 SubtitleStamp
        /// </summary>
        public string 是否中字的表现形式 { get; set; } = string.Empty;

        /// <summary>
        /// 是否流出的表现形式 DivulgedStamp
        /// </summary>
        public string 是否流出的表现形式 { get; set; } = string.Empty;

        /// <summary>
        /// 是否破解的表现形式 CrackedStamp
        /// </summary>
        public string 是否破解的表现形式 { get; set; } = string.Empty;

        public string DatePattern { get; set; } = "yyyy-MM-dd";
    }

    public class VideoSettings
    {
        /// <summary>
        /// 是否重命名视频 NeedRename
        /// </summary>
        public bool 是否重命名视频 { get; set; }

        /// <summary>
        /// 重命名视频文件的公式 NameVideoFormula
        /// </summary>
        public List<string> 重命名视频文件的公式 { get; set; } = new List<string>();

        /// <summary>
        /// 扫描视频文件类型 VideoTypes
        /// </summary>
        public List<string> 扫描文件类型 { get; set; } = new List<string>();
    }

    public class SubtitleSettings
    {
        /// <summary>
        /// 是否重命名用户已拥有的字幕
        /// </summary>
        public bool 是否重命名字幕 { get; set; }

        public bool SkipCollectSubtitleIfExist { get; set; }
    }

    public class ClassifySettings
    {
        /// <summary>
        /// 归类根目录 ClassifyRootDir
        /// </summary>
        public string 归类根目录 { get; set; } = string.Empty;

        /// <summary>
        /// 归类方式 ClassifyOperationType
        /// </summary>
        [JsonConverter(typeof(ClassifyOperationTypeConverter))]
        public ClassifyOperationType 归类方式 { get; set; }

        /// <summary>
        /// 归类的标准 ClassifyRelativePathFormula
        /// </summary>
        public List<string> 归类的标准 { get; set; } = new List<string>();

        /// <summary>
        /// 是否需要独立文件夹
        /// </summary>
        /// <returns></returns>
        public bool JudgeNeedSeparateFolder()
        {
            List<string> 父文件夹命名公式 = 归类的标准[(归类的标准.LastIndexOf("\\") + 1)..];
            return 父文件夹命名公式.Contains(nameof(AssembleDto.车牌));
        }
    }

    public class NfoSettings
    {
        /// <summary>
        /// 是否收集nfo Need
        /// </summary>
        public bool 是否需要 { get; set; }

        public bool NeedZhPlot { get; set; }
        public bool NeedJavLibraryReview { get; set; }

        /// <summary>
        /// title的公式 NfoTitleFormula
        /// </summary>
        public List<string> Title的公式 { get; set; } = new List<string>();

        /// <summary>
        /// 额外增加以下元素到特征中 ExtraCollectProperties
        /// </summary>
        public List<string> 额外增加以下元素到特征中 { get; set; } = new List<string>();
    }

    public class FanartSettings
    {
        public bool 是否需要fanart { get; set; }

        /// <summary>
        /// fanart的公式 NameFanartFormula
        /// </summary>
        public List<string> Fanart的公式 { get; set; } = new List<string>();

        public bool 是否需要poster { get; set; }

        /// <summary>
        /// poster的公式 NamePosterFormula
        /// </summary>
        public List<string> Poster的公式 { get; set; } = new List<string>();

        public bool NeedSubtitleWatermark { get; set; }
        public bool NeedDivulgeWatermark { get; set; }
        public bool NeedLocateFacial { get; set; }
    }

    public class PosterSettings
    {
    }

    public class EmbySettings
    {
        public string Server { get; set; } = string.Empty;
        public string ApiId { get; set; } = string.Empty;
        public bool OverwritePreviousHeadSculpture { get; set; }
    }

    public class KodiSettings
    {
        /// <summary>
        /// 是否对多cd只收集一份图片和nfo OnlyOneWhenCDs
        /// </summary>
        public bool 是否对多cd只收集一份图片和nfo { get; set; }

        public bool NeedHeadSculpture { get; set; }
    }
}