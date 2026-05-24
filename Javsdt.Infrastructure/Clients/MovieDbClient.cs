using HappreeTool.ApiAbouts.Messages;
using Javsdt.Domain.Entitys;
using Javsdt.Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Javsdt.Infrastructure.Clients
{
    public class MovieDbClient(ILogger<MovieDbClient> logger,
                               IHttpClientFactory _httpClientFactory,
                               IOptions<AvpiSettings> options)
    {
        private readonly string baseUrl = options.Value.BaseUrl.TrimEnd('/');

        public string BuildMovieDetailUrl(string movieId) => $"{baseUrl}/movies/{movieId}";

        public string BuildMoviePosterUrl(string movieId) => $"{baseUrl}/movies/{movieId}/poster";

        public string BuildMovieFanartUrl(string movieId) => $"{baseUrl}/movies/{movieId}/fanart";

        public string BuildMovieSearchUrl(string code) => $"{baseUrl}/movies?code={Uri.EscapeDataString(code)}";

        /// <summary>
        /// 根据code查影片
        /// </summary>
        /// <param name="code"></param>
        /// <returns>List<Movie>，查不到则为空list</returns>
        public async Task<List<Movie>> GetDetail(string code)
        {
            string getMovieByCodeUrl = BuildMovieSearchUrl(code);
            HttpResponseMessage response = await _httpClientFactory.CreateClient().GetAsync(getMovieByCodeUrl);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            ApiResponseMessage<List<Movie>> message = JsonSerializer.Deserialize<ApiResponseMessage<List<Movie>>>(jsonResponse)!;
            return message.Data!;
        }

        /// <summary>
        /// 获取fanart的byte[]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<byte[]> GetFanartBytes(string id)
        {
            string url = BuildMovieFanartUrl(id);
            logger.LogInformation("准备从MovieDb服务【{url}】获取fanart的文件资源bytes", url);

            HttpResponseMessage response = await _httpClientFactory.CreateClient().GetAsync(url);
            //if (response.StatusCode == HttpStatusCode.NotFound)
            //{
            //    return null;
            //}
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// 获取poster的byte[]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<byte[]> GetPosterBytes(string id)
        {
            string url = BuildMoviePosterUrl(id);
            logger.LogInformation("准备从MovieDb服务【{url}】获取poster的文件资源bytes", url);

            HttpResponseMessage response = await _httpClientFactory.CreateClient().GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
