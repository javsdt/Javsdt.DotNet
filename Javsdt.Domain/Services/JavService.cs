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

        public int GetTotalCount()
        {
            return _repository.GetTotalCount();
        }

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
