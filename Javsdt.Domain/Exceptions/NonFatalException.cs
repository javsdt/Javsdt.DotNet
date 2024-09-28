using Javsdt.Shared.Constants;

namespace Javsdt.Domain.Exceptions
{
    /// <summary>
    /// 处理过程中，非致命的错误，可以放弃当前项，继续下一项整理，即使后续仍会发生这种错误。
    /// </summary>
    /// <param name="message"></param>
    public class NonFatalException(string message) : Exception(message) { }

    /// <summary>
    /// 暂时不支持部分类型车牌的整理
    /// </summary>
    public class NotSupportedJavTypeException(string type, string carName)
        : NonFatalException(ExceptionMessage.NOT_CURRENT_SUPPORTED_BUSINESS(type, carName))
    { }

    /// <summary>
    /// 【步骤11】移动文件夹出错
    /// </summary>
    /// <param name="targetPath"></param>
    public class MoveFileWhenRenameException(string currentPath, string targetPath, string message) :
        NonFatalException(ExceptionMessage.MOVE_FILE_WHEN_RENAME(currentPath, targetPath, message))
    { }

    /// <summary>
    /// 【步骤11】移动文件夹出错
    /// </summary>
    /// <param name="targetPath"></param>
    public class MoveFileWhenClassifyException(string currentPath, string targetPath, string message) :
        NonFatalException(ExceptionMessage.MOVE_FILE_WHEN_CLASSIFY(currentPath, targetPath, message))
    { }

    /// <summary>
    /// 【步骤11】影片存在于独立文件夹中，避免在这个文件夹中又创建独立文件夹
    /// </summary>
    public class NestingDollCreateSeparateFolderException(string dir) :
        NonFatalException(ExceptionMessage.NESTING_DOLL_CREATE_SEPARATE_FOLDER(dir))
    { }

    /// <summary>
    /// 【步骤11】目标文件夹已存在
    /// </summary>
    /// <param name="targetDir"></param>
    public class ClassifyDirAlreadyExistException(string targetDir) :
        NonFatalException(ExceptionMessage.CLASSIFY_DIR_ALREADY_EXIST(targetDir))
    { }

    /// <summary>
    /// 【步骤11】移动文件夹出错
    /// </summary>
    /// <param name="targetDir"></param>
    public class MoveDirectoryWhenClassifyException(string currentDir, string targetDir, string message) :
        NonFatalException(ExceptionMessage.MOVE_DIRECTORY_WHEN_CLASSIFY(currentDir, targetDir, message))
    { }

    /// <summary>
    /// 【步骤11】目标文件夹已存在
    /// </summary>
    /// <param name="targetPath"></param>
    public class ClassifyVideoAlreadyExistException(string targetPath) :
        NonFatalException(ExceptionMessage.CLASSIFY_VIDEO_ALREADY_EXIST(targetPath))
    { }

    /// <summary>
    /// 【依次尝试下载图片集】所有图片都下载失败
    /// </summary>
    /// <param name="targetDir"></param>
    public class DownloadImageUrlsException(List<string> imageUrls, string savePath) :
        NonFatalException(ExceptionMessage.DOWNLOAD_IMAGE_URLS(imageUrls, savePath))
    { }

    /// <summary>
    /// MovieDb找不到资源
    /// </summary>
    /// <param name="url"></param>
    public class MovieDbNotFoundException(string url) :
        NonFatalException(ExceptionMessage.MOVIEDB_NOT_FOUND_EXCEPTION(url))
    { }

}
