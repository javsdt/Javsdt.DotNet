using Javsdt.Application.Interfaces;
using Javsdt.Domain.Entitys;
using Javsdt.Infrastructure.Persistence;

namespace Javsdt.Infrastructure.Repositorys
{
    internal class SubtitleRepository(JavsdtContext _context) : ISubtitleRepository
    {
        public void AddRange(List<Subtitle> notBelongedSubtitles)
        {
            _context.AddRange(notBelongedSubtitles);
            _context.SaveChanges();
        }
    }
}
