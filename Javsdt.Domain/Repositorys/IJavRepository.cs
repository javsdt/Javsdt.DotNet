using Javsdt.Domain.Entitys;

namespace Javsdt.Domain.Repositorys
{
    public interface IJavRepository
    {
        void AddRange(List<Jav> javs);

        /// <summary>
        /// 清空所有
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取总数
        /// </summary>
        int GetTotalCount();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNo">跳过数量</param>
        /// <param name="batchSize">一页的数量</param>
        /// <returns></returns>
        List<Jav> GetPagedResultsAsync(int pageNo, int batchSize);

        /// <summary>
        /// 更新所在文件夹
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newDir"></param>
        void UpdateDirectory(int id, string newDir);

        /// <summary>
        /// 标记错误：同车牌的视频分散在不同文件夹
        /// </summary>
        void UpdateInDifferentFoldersStatus();

        /// <summary>
        /// 更新不带扩展名的基本文件名
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="newNameWithoutExt">新的不带扩展名的基本文件名</param>
        void UpdateNameWithoutExt(int id, string newNameWithoutExt);
    }
}