using Javsdt.Domain.Configuration;
using Javsdt.Domain.Entitys;
using Javsdt.Domain.Exceptions;
using Javsdt.Shared.Constants;
using Javsdt.Shared.Enums;

namespace Javsdt.Domain.Dtos
{
    public class AssembleDto
    {
        /// <summary>
        /// 用于文件命名的jav信息
        /// </summary>
        /// <param name="jav"></param>
        /// <param name="movie"></param>
        public AssembleDto(Jav jav, Movie movie, StandardSettings settings)
        {
            int 标题长度限制 = settings.Element.标题长度限制;
            _jav = jav;

            车牌 = movie.Code;
            车头 = movie.CodePref;
            标题 = movie.Title;
            用于文件命名的标题 = 标题.Length > 标题长度限制 ? 标题[..标题长度限制] : 标题;
            中文标题 = movie.ZhTitle ?? 标题;
            用于文件命名的中文标题 = 中文标题.Length > 标题长度限制 ?  中文标题[..标题长度限制] : 中文标题;
            发行日期 = movie.Release?.ToString("yyyy-MM-dd") ?? MediaConstant.DEFAULT_RELEASE;
            时长 = movie.Runtime;
            评分 = movie.Score;
            Jav类型 = movie.Type;
            系列 = movie.Series ?? $"{Jav类型}系列";
            制造商们 = movie.Makers.Count != 0 ? string.Join(" ", movie.Makers.Take(7)) : $"{Jav类型}制造商";
            发行商们 = movie.Publishers.Count != 0 ? string.Join(" ", movie.Publishers.Take(7)) : $"{Jav类型}发行商";
            首个女演员 = movie.Actress.Count != 0 ? movie.Actress.First() : $"{Jav类型}演员";
            女演员们 = movie.Actress.Count != 0 ? string.Join(" ", movie.Actress.Take(7)) : $"{Jav类型}演员";
            导演们 = movie.Directors.Count != 0 ? string.Join(" ", movie.Directors.Take(1)) : $"{Jav类型}导演";
            中文字幕标记 = jav.HasSubtitle ? settings.Element.是否中字的表现形式 : string.Empty;
            无码流出标记 = jav.IsDivulged ? settings.Element.是否流出的表现形式 : string.Empty;
            AI破解标记 = jav.IsCracked ? settings.Element.是否破解的表现形式 : string.Empty;
        }

        private readonly Jav _jav;

        public string 车牌 { get; set; }

        public string 车头 { get; private set; }

        public string 标题 { get; private set; }

        /// <summary>
        /// 用于文件命名的标题 TitleInFile
        /// </summary>
        public string 用于文件命名的标题 { get; private set; }

        /// <summary>
        /// 简中标题 ZhTitle
        /// </summary>
        /// <remarks>没翻译则为原标题</remarks>
        public string 中文标题 { get; private set; }

        /// <summary>
        /// 用于文件命名的中文标题 ZhTitleInFile
        /// </summary>
        /// <remarks>没翻译则为原标题</remarks>
        public string 用于文件命名的中文标题 { get; private set; }

        /// <summary>
        /// 发行日期 Release
        /// </summary>
        public string 发行日期 { get; private set; }

        /// <summary>
        /// 时长 Runtime
        /// </summary>
        public int 时长 { get; private set; }

        /// <summary>
        /// 评分 Score
        /// </summary>
        /// <remarks>百分制</remarks>
        public int 评分 { get; private set; }

        /// <summary>
        /// Jav类型 Type
        /// </summary>
        public JavType Jav类型 { get; private set; }

        /// <summary>
        /// 系列 Series
        /// </summary>
        public string? 系列 { get; private set; }

        /// <summary>
        /// 制造商们 Makers
        /// </summary>
        public string? 制造商们 { get; private set; }

        /// <summary>
        /// 发行商们 Publishers
        /// </summary>
        public string? 发行商们 { get; private set; }

        /// <summary>
        /// 首个女演员 FirstActress
        /// </summary>
        public string 首个女演员 { get; private set; }

        /// <summary>
        /// 演员们 Actresses
        /// </summary>
        public string 女演员们 { get; private set; }

        /// <summary>
        /// 导演们 Directors
        /// </summary>
        public string 导演们 { get; private set; }

        /// <summary>
        /// 中文字幕标记 SubtitleStamp
        /// </summary>
        public string 中文字幕标记 { get; private set; }

        /// <summary>
        /// 无码流出标记 DivulgedStamp
        /// </summary>
        public string 无码流出标记 { get; private set; }

        /// <summary>
        /// AI破解标记 CrackedStamp
        /// </summary>
        public string AI破解标记 { get; private set; }

        /// <summary>
        /// 当前文件名不带扩展名 NameWithoutExt
        /// </summary>
        public string 不带扩展的文件名 => _jav.NameWithoutExt;

        /// <summary>
        /// 原文件夹名 OriginFolder
        /// </summary>
        public string 原文件夹名 => Path.GetFileName(_jav.OriginDir) ?? throw new GetFileNameException(_jav.OriginDir);

        /// <summary>
        /// 车牌首字母 FirstLetter
        /// </summary>
        public string 车牌首字母 => 车头[0].ToString();
    }
}
