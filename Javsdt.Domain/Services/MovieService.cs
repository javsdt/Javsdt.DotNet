using Javsdt.Domain.Entitys;
using Javsdt.Domain.Repositorys;

namespace Javsdt.Domain.Services
{
    public class MovieService(IMovieRepository repository)
    {
        public void DownloadFanart(string car, string fanartPath)
        {
            byte[] fanartBytes = repository.GetFanartBytes(car).Result;
            File.WriteAllBytes(fanartPath, fanartBytes);
        }

        public void DownloadPoster(string car, string posterPath)
        {
            byte[] posterBytes = repository.GetPosterBytes(car).Result;
            File.WriteAllBytes(posterPath, posterBytes);
        }

        public Movie? GetDetail(string car)
        {
            return repository.GetDetail(car);
        }
    }
}
