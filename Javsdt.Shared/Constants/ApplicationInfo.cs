using System.Text.RegularExpressions;

namespace MovieDb.Shared.Constants
{
    public static class ApplicationInfo
    {
        public static string AppName { get; } = "Javsdt";
        public static string CurrentDirectory { get; } = Environment.CurrentDirectory;
        public static string UserDirectory { get; } = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public static string TempDirectory { get; } = Path.GetTempPath();

        public static string AppSettingsJsonPath { get; } = GetAppSettingsJsonPath();

        private static string GetAppSettingsJsonPath()
        {
            string projectRoot = Regex.Match(CurrentDirectory, @"^.*?Javsdt.DotNet").Value;
            return Path.Combine(projectRoot, "Javsdt.Application", "appsettings.Development.json");
        }

    }
}
