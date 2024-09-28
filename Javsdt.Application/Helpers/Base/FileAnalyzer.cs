using HappreeTool.CommonUtils;
using HappreeTool.Documents;
using Javsdt.Shared.Configuration;
using Javsdt.Domain.Entitys;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Javsdt.Application.Helpers.Base
{
    public class FileAnalyzer(ILogger<FileAnalyzer> _logger)
    {
        /// <summary>
        /// 检查视频自身显而易见的特征
        /// </summary>
        /// <remarks>（1）文件名带中字（2）同名的nfo标记了特征（3）</remarks>
        public void DiscoverObviousFeatures(Jav jav)
        {
            //_logger.LogInformation("【检查视频自身显而易见的特征】begin");

            // 判断是否有中字的特征，条件有三满足其一即可:
            // 1、文件名中含有“-C”之类的字眼 2、旧的nfo中已经记录了它的中字特征
            UpdateHasSubtitleByFileNameAndNfo(jav);

            // 判断是否是无码流出的作品，同理
            UpdateIsDivluged(jav);

            // 判断是否是AI破解的作品，同理
            UpdateIsCrackedByFileNameAndNfo(jav);

            // 用以上属性给【版本】一个默认值
            UpdateDefaultEdition(jav);

            //_logger.LogInformation("【检查视频自身显而易见的特征】end");
        }

        /// <summary>
        /// 依据文件名和旧nfo判定是否有字幕
        /// </summary>
        private static void UpdateHasSubtitleByFileNameAndNfo(Jav jav)
        {
            if (jav.HasSubtitle) return;

            // 去除 '-CD' 和 '-CARIB'对 '-C'判断中字的影响
            string nameWithoutExt = MyStringUtils.ReplaceByArray(
                jav.NameWithoutExt, SettingsHolder.Standard.Birthmark.InterfereSubtitleWords);
            // 如果原文件名包含“-c、-C、中字”这些字符
            foreach (var word in SettingsHolder.Standard.Birthmark.SubtitleWords)
            {
                if (!string.IsNullOrEmpty(word) && nameWithoutExt.Contains(word))
                {
                    jav.HasSubtitle = true;
                    return;
                }
            }

            // 可能存在的旧nfo路径，有 “中文字幕”这个Genre
            string pathNfo = $"{jav.Dir}/{jav.NameWithoutExt}.nfo";
            if (File.Exists(pathNfo))
            {
                if (XmlUtils.ExistExpectedTextInSpecificNode(pathNfo, "movie/genre", "中文字幕"))
                {
                    jav.HasSubtitle = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 依据文件名和旧nfo判定是否无码流出
        /// </summary>
        private static void UpdateIsDivluged(Jav jav)
        {
            if (jav.IsDivulged) return;

            string nameWithoutExt = MyStringUtils.ReplaceByArray(
                jav.NameWithoutExt, SettingsHolder.Standard.Birthmark.InterfereDivulgedWords);
            // 如果原文件名包含“无码流出”这些字符
            foreach (var word in SettingsHolder.Standard.Birthmark.DivulgedWords)
            {
                if (!string.IsNullOrEmpty(word) && nameWithoutExt.Contains(word))
                {
                    jav.IsDivulged = true;
                    return;
                }
            }

            // 可能存在的旧nfo路径，有 “无码流出”这个Genre
            string pathNfo = $"{jav.Dir}/{jav.NameWithoutExt}.nfo";
            if (File.Exists(pathNfo))
            {
                if (XmlUtils.ExistExpectedTextInSpecificNode(pathNfo, "movie/genre", "无码流出"))
                {
                    jav.IsDivulged = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 依据文件名和旧nfo判定是否AI破解
        /// </summary>
        private static void UpdateIsCrackedByFileNameAndNfo(Jav jav)
        {
            if (jav.IsCracked) return;


            string nameWithoutExt = MyStringUtils.ReplaceByArray(
                jav.NameWithoutExt, SettingsHolder.Standard.Birthmark.InterfereCrackedWords);
            // 如果原文件名包含“无码流出”这些字符
            foreach (var word in SettingsHolder.Standard.Birthmark.CrackedWords)
            {
                if (!string.IsNullOrEmpty(word) && nameWithoutExt.Contains(word))
                {
                    jav.IsCracked = true;
                    return;
                }
            }

            // 可能存在的旧nfo路径，有 “AI破解”这个Genre
            string pathNfo = $"{jav.Dir}/{jav.NameWithoutExt}.nfo";
            if (File.Exists(pathNfo))
            {
                if (XmlUtils.ExistExpectedTextInSpecificNode(pathNfo, "movie/genre", "AI破解"))
                {
                    jav.IsCracked = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 依据是否有外挂字幕判断是否中字
        /// </summary>
        /// <remarks>（1）文件名带中字（2）同名的nfo标记了特征（3）</remarks>
        public static void UpdateHasSubtitlByHangingSubtitle(Jav jav)
        {
            if (jav.HasSubtitle) return;

            // 视频旁边有字幕
            jav.HasSubtitle = jav.Subtitles.Any();
        }

        /// <summary>
        /// 依据3个属性给当前jav一个默认Edition
        /// </summary>
        public static void UpdateDefaultEdition(Jav jav)
        {
            // 初始化一个空的字符串构建器
            var editionBuilder = new StringBuilder();

            // 根据条件附加相应的字符串
            if (jav.HasSubtitle)
            {
                editionBuilder.Append($" {SettingsHolder.Standard.Element.SubtitleStamp}");
            }

            if (jav.IsDivulged)
            {
                editionBuilder.Append($" {SettingsHolder.Standard.Element.DivulgedStamp}");
            }

            if (jav.IsCracked)
            {
                editionBuilder.Append($" {SettingsHolder.Standard.Element.CrackedStamp}");
            }

            // 设置Edition属性为构建好的字符串，并去除首尾的空白字符
            string edition = editionBuilder.ToString().Trim();
            if (edition.Length > 0)
            {
                jav.Edition = edition;
            }
        }
    }
}
