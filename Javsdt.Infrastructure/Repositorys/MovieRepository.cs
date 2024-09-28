using Javsdt.Domain.Entitys;
using Javsdt.Domain.Repositorys;
using Javsdt.Infrastructure.Clients;

namespace Javsdt.Infrastructure.Repositorys
{
    internal class MovieRepository(MovieDbClient client) : IMovieRepository
    {
        public Movie? GetDetail(string car)
        {
            return client.GetDetail(car).Result;
        }

        public async Task<byte[]> GetFanartBytes(string car)
        {
            return await client.GetFanartBytes(car);
        }

        public async Task<byte[]> GetPosterBytes(string car)
        {
            return await client.GetPosterBytes(car);
        }
    }
}
