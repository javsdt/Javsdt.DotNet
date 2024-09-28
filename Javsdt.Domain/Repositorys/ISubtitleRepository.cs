using Javsdt.Domain.Entitys;

namespace Javsdt.Application.Interfaces
{
    public interface ISubtitleRepository
    {
        void AddRange(List<Subtitle> notBelongedSubtitles);
    }
}
