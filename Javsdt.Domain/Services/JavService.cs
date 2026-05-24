using Javsdt.Domain.Entitys;
using Javsdt.Domain.Repositorys;

namespace Javsdt.Domain.Services
{
    public class JavService(IJavRepository _repository)
    {
        public void Clear()
        {
            _repository.Clear();
        }

        /// <summary>
        /// 获取所有要处理的jav的数量
        /// </summary>
        /// <returns></returns>
        public int CountAllToHandle()
        {
            return _repository.CountAll();
        }

        /// <summary>
        /// 分页获取要处理的所有jav
        /// </summary>
        /// <param name="skipNum">跳过数量</param>
        /// <param name="batchSize">第几页</param>
        /// <returns></returns>
        public List<Jav> GetPagedResultsAsync(int skipNum, int batchSize)
        {
            return _repository.GetPagedResultsAsync(skipNum, batchSize);
        }

        public void AddRange(List<Jav> javs)
        {
            _repository.AddRange(javs);
        }

        public void UpdateInDifferentFoldersStatus()
        {
            _repository.UpdateInDifferentFoldersStatus();
        }

        public void UpdateNameWithoutExt(int id, string newNameWithoutExt)
        {
            _repository.UpdateNameWithoutExt(id, newNameWithoutExt);
        }

        public void UpdateDirectory(int id, string targetDir)
        {
            _repository.UpdateDirectory(id, targetDir);
        }
    }
}
