
namespace Javsdt.Shared.Constants
{
    public static class ExceptionMessage
    {

        public static string NOT_CURRENT_SUPPORTED_BUSINESS(string type, string carName) => $"暂不支持的【{type}】类型的业务操作：【{carName}】";

        public static string UNKNOWN_WEBSITE_CONTENT(string url) => $"打开：{url} 网页成功，但无法核验网页内容，请联系作者！";

        public static string IP_BANNED(string url, string website) => $"打开：{url} 网页失败，你的ip已被网站【{website}】封禁";

        public static string NESTING_DOLL_CREATE_SEPARATE_FOLDER(string dir) =>
            $"【归类目录】不建议在当前文件夹内再新建文件夹【{dir}】";

        public static string CLASSIFY_DIR_ALREADY_EXIST(string targetDir) =>
            $"【归类目录】归类的目标位置已存在相同文件夹:【{targetDir}】";

        public static string CLASSSIFY_ROOT_ON_ANOTHER_DISK_EXCEPTION(string classifyRootDir) =>
            $"归类的根目录: 【{classifyRootDir}】与所选文件夹不在同一磁盘，无法归类！请修正后重启程序！";

        public static string CLASSSIFY_ROOT_DIR_NOT_EXIST(string classifyRootDir) =>
            $"归类的根目录: 【{classifyRootDir}】不存在！无法归类！请修正后重启程序！";

        public static string MOVE_DIRECTORY_WHEN_CLASSIFY(string currentDir, string targetDir, string message) =>
            $"【归类目录】移动文件夹时发生错误: {currentDir} -> {targetDir}\n错误信息: {message}";

        public static string MOVE_FILE_WHEN_CLASSIFY(string currentPath, string targetPath, string message) =>
            $"【归类视频】移动视频文件时发生错误: {currentPath} -> {targetPath}\n错误信息: {message}";

        public static string MOVE_FILE_WHEN_RENAME(string currentPath, string targetPath, string message) =>
            $"【重命名视频】移动视频文件时发生错误: {currentPath} -> {targetPath}\n错误信息: {message}";

        public static string DOWNLOAD_IMAGE_URLS(List<string> imageUrls, string savePath) => $"下载图片{imageUrls}到【{savePath}】全部失败";

        public static string GET_DIRECTORY(string path) => $"无法获取父目录，对于【{path}】";

        public static string GET_FILE_NAME(string path) => $"无法获取完整文件名，对于【{path}】";

        public static string CLASSIFY_VIDEO_ALREADY_EXIST(string targetPath) =>
            $"【归类视频】目标位置已存在同名文件:【{targetPath}】";

        public static string UNKNOWN_OPERATION(string opreation) => $"意料之外的操作:【{opreation}】";

        public static string MOVIEDB_NOT_FOUND_EXCEPTION(string url) => $"MovieDb找不到资源:【{url}】";
    }
}
