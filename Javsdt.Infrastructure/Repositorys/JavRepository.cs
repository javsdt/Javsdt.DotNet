using Javsdt.Domain.Entitys;
using Javsdt.Domain.Repositorys;
using Javsdt.Infrastructure.Persistence;
using Javsdt.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Javsdt.Infrastructure.Repositorys
{
    internal class JavRepository(JavsdtContext _context) : IJavRepository
    {
        private readonly DbSet<Jav> _records = _context.Javs;

        public void Clear()
        {
            _context.DeleteAllData<Jav>();
            _context.DeleteAllData<Subtitle>();
        }

        public int GetTotalCount()
        {
            return _records.Count();
        }

        public List<Jav> GetPagedResultsAsync(int pageNo, int batchSize)
        {
            return _records.AsNoTracking()
                .Include(j => j.Subtitles)
                .OrderBy(j => j.Id).Skip(batchSize * pageNo).Take(batchSize)
                .ToList();
        }

        public void UpdateNameWithoutExt(int id, string newNameWithoutExt)
        {
            _records.Where(j => j.Id == id)
                .ExecuteUpdate(setters => setters.SetProperty(j => j.NameWithoutExt, newNameWithoutExt));
        }

        public void UpdateDirectory(int id, string newDir)
        {
            _records.Where(j => j.Id == id)
                        .ExecuteUpdate(setters => setters.SetProperty(j => j.Dir, newDir));
        }

        public void UpdateInDifferentFoldersStatus()
        {
            List<Jav> javsToUpdate = _records
                .Where(record => _records.Any(another => another.Car == record.Car && another.OriginDir != record.OriginDir))
                .ToList();

            foreach (Jav jav in javsToUpdate)
            {
                jav.Status = CrawlStatus.同一影片分布在不同文件夹;
            }

            _context.SaveChanges();
        }

        public void AddRange(List<Jav> javs)
        {
            _records.AddRange(javs);
            _context.SaveChanges();
        }
    }
}