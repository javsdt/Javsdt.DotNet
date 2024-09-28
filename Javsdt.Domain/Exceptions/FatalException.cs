using Javsdt.Shared.Constants;

namespace Javsdt.Domain.Exceptions
{
    /// <summary>
    /// 处理过程中，发生致命错误，必须停止整个整理过程终止，否则会大量报错或污染数据。
    /// </summary>
    public class FatalException(string message) : Exception(message) { }

    /// <summary>
    /// 用户设置的归类根目录与用户要整理的目录不在同一磁盘
    /// </summary>
    public class ClasssifyRootDirOnAnotherDiskException(string classifyRootDir)
        : FatalException(ExceptionMessage.CLASSSIFY_ROOT_ON_ANOTHER_DISK_EXCEPTION(classifyRootDir))
    { }

    /// <summary>
    /// 用户设置的归类根目录不存在
    /// </summary>
    public class ClasssifyRootDirNotExistException(string classifyRootDir)
        : FatalException(ExceptionMessage.CLASSSIFY_ROOT_DIR_NOT_EXIST(classifyRootDir))
    { }

    /// <summary>
    /// 无法获取所在父目录
    /// </summary>
    public class GetDirectoryException(string path)
        : FatalException(ExceptionMessage.GET_DIRECTORY(path))
    { }

    /// <summary>
    /// 无法获取完整文件名
    /// </summary>
    public class GetFileNameException(string path)
        : FatalException(ExceptionMessage.GET_FILE_NAME(path))
    { }

    /// <summary>
    /// 意料外的操作
    /// </summary>
    /// <param name="message"></param>
    public class UnknownOperationException(string message)
        : FatalException(message)
    { }

    /// <summary>
    /// MovieDb服务器错误
    /// </summary>
    /// <param name="message"></param>
    public class MovieDbServerException(string message)
        : FatalException(message)
    { }

}
