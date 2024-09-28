using Javsdt.Domain.Entitys;
using Microsoft.Extensions.Configuration;

namespace Javsdt.Infrastructure.Clients
{
    public class MovieDbClient(HttpClientWrapper httpClientWrapper, IConfiguration configuration)
    {
        private string FormatGetDetailUrl(string car) => string.Format(configuration["ThirdPartys:MovieDb:Movie:GetDetail"]!, car);
        private string FormatGetFanartUrl(string car) => string.Format(configuration["ThirdPartys:MovieDb:Movie:GetFanart"]!, car);
        private string FormatGetPosterUrl(string car) => string.Format(configuration["ThirdPartys:MovieDb:Movie:GetPoster"]!, car);

        public async Task<Movie?> GetDetail(string car)
        {
            Movie? movie = await httpClientWrapper.GetJsonAsync<Movie>(FormatGetDetailUrl(car));
            return movie;
        }

        internal async Task<byte[]> GetFanartBytes(string car)
        {
            string fanartUrl = FormatGetFanartUrl(car);
            return await httpClientWrapper.GetBytesAsync(fanartUrl);
        }

        internal async Task<byte[]> GetPosterBytes(string car)
        {
            return await httpClientWrapper.GetBytesAsync(FormatGetPosterUrl(car));
        }
    }
}
