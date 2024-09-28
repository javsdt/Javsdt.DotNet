using Javsdt.Application.Interfaces;
using Javsdt.Domain.Entitys;

namespace Javsdt.Domain.Services
{
    public class SubtitleService(ISubtitleRepository repository)
    {
        public void AddRange(List<Subtitle> notBelongedSubtitles)
        {
            repository.AddRange(notBelongedSubtitles);
        }
    }
}
