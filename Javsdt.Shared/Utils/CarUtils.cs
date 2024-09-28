using HappreeTool.CommonUtils;
using System.Text.RegularExpressions;

namespace Javsdt.Shared.Utils.Metadata
{
    public partial class CarUtils
    {
        [GeneratedRegex(@"([A-Z]+)[-_ ]*(\d\d+)")]
        public static partial Regex Simpleegex();

        /// <summary>
        /// T28-123
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"[^A-Z]?(T28)[-_ ]*(\d\d+)")]
        private static partial Regex T28Regex();

        /// <summary>
        /// 26ID-020
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"[^\d]?(\d\dID)[-_ ]*(\d\d+)")]
        private static partial Regex XXIDXXRegex();

        /// <summary>
        /// 一般车牌
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"([A-Z]+)[-_ ]*(\d\d+)")]
        private static partial Regex ABC123Regex();

        /// <summary>
        /// 看不懂了，但有它的意义
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"([A-Z]+)[-_ ](\d+)")]
        private static partial Regex ABC_123Regex();

        /// <summary>
        /// N123
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"[^A-Z]?(N\d\d+)")]
        private static partial Regex N123Regex();

        /// <summary>
        /// 数字_数字
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"(\d+)[-_ ](\d\d+)")]
        private static partial Regex No_NoRegex();

        /// <summary>
        /// 数字字母数字字母
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex(@"([A-Z0-9]+)([-_ ]*)([A-Z0-9]+)")]
        private static partial Regex A1B2C3Regex();

        /// <summary>
        /// 素人车牌AB-123
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("([A-Z][A-Z]+)[-_ ]*(\\d\\d+)")]
        private static partial Regex SurenRegex();

        /// <summary>
        /// FC2车牌
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("FC2[^\\d]*(\\d+)")]
        private static partial Regex Fc2Regex();

        /// <summary>
        /// 车牌的车头
        /// </summary>
        [GeneratedRegex("([a-zA-Z]+)\\d")]
        private static partial Regex PrefixRegex();

        /// <summary>
        /// 带横杠车牌的车尾
        /// </summary>
        [GeneratedRegex("-(\\d+)\\w*")]
        private static partial Regex BarSuffixRegex();

        /// <summary>
        /// 不带横杠车牌的车尾
        /// </summary>
        [GeneratedRegex("(\\d+)\\w*")]
        private static partial Regex SuffixRegex();

        /// <summary>
        /// -123tk
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("-(\\d+)(\\w*)")]
        private static partial Regex BarNoTkRegex();

        /// <summary>
        /// 123tk
        /// </summary>
        /// <returns></returns>
        [GeneratedRegex("(\\d+)(\\w*)")]
        private static partial Regex NoTkRegex();

        /// <summary>
        /// 有码正则表达式集合
        /// </summary>
        private static readonly Regex[] youmaRegexArray = [T28Regex(), XXIDXXRegex(), ABC123Regex(), ABC_123Regex()];


        /// <summary>
        /// 找出文件名中的FC2车牌
        /// </summary>
        /// <param name="fileName">视频文件基本名</param>
        /// <param name="car">找到的车牌</param>
        public static bool TryExtractFc2Car(string fileName, out string? car)
        {
            Match carg = Fc2Regex().Match(fileName.ToUpper());
            if (carg.Success)
            {
                car = $"FC2-{carg.Groups[1].Value}";
                return true;
            }

            car = null;
            return false;
        }


        /// <summary>
        /// 找出文件名中的【正常】车牌
        /// </summary>
        /// <remarks>形如ABC-123，不论是有码、素人、无码</remarks>
        /// <param name="fileName">全大写的文件名，例如: AVOP-127.MP4</param>
        /// <param name="car">找到的车牌，示例: 26ID-020，ABC-123</param>
        public static bool TryExtractCommonCar(string fileName, List<string> replaceWords, out string? car)
        {
            string cleanedName = Regex.Replace(fileName, string.Join('|', replaceWords), string.Empty, RegexOptions.IgnoreCase);
            // 尝试匹配四种不同的车牌格式
            foreach (Regex regex in youmaRegexArray)
            {
                Match carGroup = regex.Match(cleanedName.ToUpper());
                if (carGroup.Success)
                {
                    car = $"{carGroup.Groups[1].Value}-{CutExtraZero(carGroup.Groups[2].Value)}";
                    return true;
                }
            }

            car = null;
            return false;
        }


        /// <summary>
        /// 找出文件名中的【乱七八糟的无码】车牌
        /// </summary>
        /// <param name="fileName">全大写的文件名，例如: CARID-123.MP4</param>
        /// <param name="car">找到的车牌，示例: ABC123ABC123，只要是字母数字，全拿着</param>
        /// <remarks>无码就不去多余0了，去了0和不去0，可能是不同结果</remarks>
        public static bool TryExtractTerribleCar(string fileName, List<string> replaceWords, out string? car)
        {
            string cleanedName = Regex.Replace(fileName, string.Join('|', replaceWords), string.Empty, RegexOptions.IgnoreCase);
            Match carg;

            // N12345
            if ((carg = N123Regex().Match(cleanedName)).Success)
            {
                car = carg.Groups[1].Value;
                return true;
            }
            // 123-12345
            else if ((carg = No_NoRegex().Match(cleanedName)).Success)
            {
                car = $"{carg.Groups[1].Value}-{carg.Groups[2].Value}";
                return true;
            }
            // 只要是字母数字-_，全拿着
            else if ((carg = A1B2C3Regex().Match(cleanedName)).Success)
            {
                car = carg.Groups[0].Value;
                return true;
            }

            car = null;
            return false;
        }


        /// <summary>
        /// 去掉太多的0
        /// </summary>
        /// <param name="suf">车尾</param>
        /// <returns>avop00027 => avop-027</returns>
        public static string CutExtraZero(string suf)
        {
            return suf.Length > 3 ? $"{suf[..^3].TrimStart('0')}{suf[^3..]}" : suf;
        }


        /// <summary>
        /// 从车牌中提取车头
        /// </summary>
        /// <param name="car">ABC-123</param>
        /// <returns>ABC</returns>
        public static string ExtractPref(string car)
        {
            if (car.Contains('-'))
            {
                return car.Split("-")[0].ToUpper();
            }
            else
            {
                Match prefg = PrefixRegex().Match(car);
                if (!prefg.Success) return string.Empty;
                return prefg.Groups[1].Value.ToUpper();
            }
        }


        /// <summary>
        /// 从车牌中提取后缀数字（车尾）
        /// </summary>
        /// <remarks>ID-26012 => 26123 ABC-012 => 12</remarks>
        /// <param name="car">ID-26012</param>
        /// <returns>26123</returns>
        public static int ExtractSufNumber(string car)
        {
            if (car.Contains('-'))
            {
                return int.Parse(BarSuffixRegex().Match(car).Groups[1].Value);
            }
            else
            {
                Match sufg = SuffixRegex().Match(car);
                if (!sufg.Success) return 0;
                return int.Parse(sufg.Groups[1].Value);
            }
        }


        /// <summary>
        /// 获取车牌的车头和车尾
        /// </summary>
        /// <param name="car">车牌</param>
        /// <returns>(ABC, 123)</returns>
        public static (string, int) ExtractPrefAndSufNumber(string car)
        {
            return (ExtractPref(car), ExtractSufNumber(car));
        }


        /// <summary>
        /// 从车牌中提取后缀（车尾）
        /// </summary>
        /// <remarks>数字加字母，并且删除多余的0</remarks>
        /// <param name="car">ABP-515tk</param>
        /// <returns>ABP-515tk => 515tk</returns>
        public static string ExtractSuf(string car)
        {
            Match sufg = car.Contains('-') ? BarNoTkRegex().Match(car) : NoTkRegex().Match(car);
            return $"{int.Parse(sufg.Groups[1].Value)}{sufg.Groups[2].Value}";
        }


        /// <summary>
        /// 删除车牌中过多的0
        /// </summary>
        /// <param name="car">ABC012tk</param>
        /// <returns>ABC-12tk</returns>
        public static string ExtractLessZeroCar(string car)
        {
            return $"{ExtractPref(car)}-{ExtractSuf(car)}";
        }
    }
}
