using Javsdt.Shared.Configuration;
using Javsdt.Domain.Entitys;
using Javsdt.Domain.Exceptions;
using Javsdt.Shared.Constants;
using Javsdt.Shared.Enums;

namespace Javsdt.Application.Dtos
{
    public class AssembleDto
    {
        public AssembleDto(Jav jav, Movie movie)
        {
            _jav = jav;

            Car = movie.Car;
            CarPref = movie.CarPref;
            Title = movie.Title;
            TitleInFile = Title.Length > SettingsHolder.Standard.Element.TitleLimit ?
                Title[..SettingsHolder.Standard.Element.TitleLimit] : Title;
            ZhTitle = movie.ZhTitle ?? Title;
            ZhTitleInFile = ZhTitle.Length > SettingsHolder.Standard.Element.TitleLimit ? 
                ZhTitle[..SettingsHolder.Standard.Element.TitleLimit] : ZhTitle;
            Release = movie.Release?.ToString("yyyy-MM-dd") ?? MediaConstant.DEFAULT_RELEASE;
            Runtime = movie.Runtime;
            Score = movie.Score;
            Type = movie.Type;
            Series = movie.Series ?? $"{Type}系列";
            Makers = movie.Makers.Count != 0 ? string.Join(" ", movie.Makers.Take(7)) : $"{Type}制造商";
            Publishers = movie.Publishers.Count != 0 ? string.Join(" ", movie.Publishers.Take(7)) : $"{Type}发行商";
            FirstActor = movie.Actors.Count != 0 ? movie.Actors.First() : $"{Type}演员";
            Actors = movie.Actors.Count != 0 ? string.Join(" ", movie.Actors.Take(7)) : $"{Type}演员";
            Directors = movie.Directors.Count != 0 ? string.Join(" ", movie.Directors.Take(1)) : $"{Type}导演";
            SubtitleStamp = jav.HasSubtitle ? SettingsHolder.Standard.Element.SubtitleStamp : string.Empty;
            DivulgedStamp = jav.IsDivulged ? SettingsHolder.Standard.Element.DivulgedStamp : string.Empty;
            CrackedStamp = jav.IsCracked ? SettingsHolder.Standard.Element.CrackedStamp : string.Empty;
        }

        private readonly Jav _jav;

        public string Car { get; set; }

        public string CarPref { get; private set; }

        public string Title { get; private set; }

        /// <summary>
        /// 文件名中的标题
        /// </summary>
        public string TitleInFile { get; private set; }

        /// <summary>
        /// 简中标题
        /// </summary>
        /// <remarks>没翻译则为原标题</remarks>
        public string ZhTitle { get; private set; }

        /// <summary>
        /// 用于文件命名的简中标题
        /// </summary>
        /// <remarks>没翻译则为原标题</remarks>
        public string ZhTitleInFile { get; private set; }

        /// <summary>
        /// 发行日期
        /// </summary>
        public string Release { get; private set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Runtime { get; private set; }

        /// <summary>
        /// 评分
        /// </summary>
        /// <remarks>百分制</remarks>
        public int Score { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public JavType Type { get; private set; }

        /// <summary>
        /// 系列
        /// </summary>
        public string? Series { get; private set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string? Makers { get; private set; }

        /// <summary>
        /// 发行商
        /// </summary>
        public string? Publishers { get; private set; }

        /// <summary>
        /// 首个演员
        /// </summary>
        public string FirstActor { get; private set; }

        /// <summary>
        /// 演员们
        /// </summary>
        public string Actors { get; private set; }

        /// <summary>
        /// 导演
        /// </summary>
        public string Directors { get; private set; }

        /// <summary>
        /// 中文字幕标记
        /// </summary>
        public string SubtitleStamp { get; private set; }

        /// <summary>
        /// 无码流出标记
        /// </summary>
        public string DivulgedStamp { get; private set; }

        /// <summary>
        /// AI破解标记
        /// </summary>
        public string CrackedStamp { get; private set; }

        /// <summary>
        /// 当前文件名不带扩展名
        /// </summary>
        public string NameWithoutExt => _jav.NameWithoutExt;

        /// <summary>
        /// 原文件夹名
        /// </summary>
        public string OriginFolder => Path.GetFileName(_jav.OriginDir) ?? throw new GetFileNameException(_jav.OriginDir);
    }
}
