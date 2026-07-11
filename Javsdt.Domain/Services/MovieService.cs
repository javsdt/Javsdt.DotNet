using Javsdt.Domain.Entitys;
using Javsdt.Domain.Repositorys;

namespace Javsdt.Domain.Services
{
    public class MovieService(IMovieRepository repository)
    {
        public async Task DownloadFanart(string car, string fanartPath)
        {
            byte[] fanartBytes = await repository.GetFanartBytes(car);
            await File.WriteAllBytesAsync(fanartPath, fanartBytes);
        }

        public async Task DownloadPoster(string car, string posterPath)
        {
            byte[] posterBytes = await repository.GetPosterBytes(car);
            await File.WriteAllBytesAsync(posterPath, posterBytes);
        }

        public Task<List<Movie>> GetDetail(string car)
        {
            return repository.GetDetail(car);
        }

        public Task<CodePref?> GetCodePref(string codePref)
        {
            return repository.GetCodePref(codePref);
        }
    }
}
