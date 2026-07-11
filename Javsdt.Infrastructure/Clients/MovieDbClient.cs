using HappreeTool.ApiAbouts.Messages;
using Javsdt.Domain.Entitys;
using Javsdt.Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using HappreeTool.ApiAbouts.Formats;
using Javsdt.Domain.Repositorys;

namespace Javsdt.Infrastructure.Clients
{
    public class MovieDbClient(ILogger<MovieDbClient> logger,
                               IHttpClientFactory httpClientFactory,
                               IOptions<AvpiSettings> options): IMovieRepository
    {
        private readonly string _baseUrl = options.Value.BaseUrl.TrimEnd('/');

        private string BuildMovieDetailUrl(string movieId) => $"{_baseUrl}/movie/{movieId}";

        private string BuildMoviePosterUrl(string movieId) => $"{_baseUrl}/movie/{movieId}/poster";

        private string BuildMovieFanartUrl(string movieId) => $"{_baseUrl}/movie/{movieId}/fanart";

        private string BuildMovieSearchUrl(string code) => $"{_baseUrl}/movie?code={Uri.EscapeDataString(code)}";

        private string BuildCodePrefUrl(string codePref) => $"{_baseUrl}/codePref/{Uri.EscapeDataString(codePref)}";

        /// <summary>
        /// 根据code查影片
        /// </summary>
        /// <param name="code"></param>
        /// <returns>查不到则为空list</returns>
        public async Task<List<Movie>> GetDetail(string code)
        {
            string getMovieByCodeUrl = BuildMovieSearchUrl(code);
            HttpResponseMessage response = await httpClientFactory.CreateClient().GetAsync(getMovieByCodeUrl);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            var message = JsonSerializer.Deserialize<ApiResponseMessage<List<Movie>>>(
                jsonResponse, JsonApiFormat.CASE_INSENSITIVE_OPTIONS)!;

            List<Movie>? movies = message.Data;
            return movies == null ? [] : movies.ToList();
        }

        /// <summary>
        /// 获取fanart的byte[]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<byte[]> GetFanartBytes(string id)
        {
            string url = BuildMovieFanartUrl(id);
            logger.LogInformation("准备从MovieDb服务【{url}】获取fanart的文件资源bytes", url);

            HttpResponseMessage response = await httpClientFactory.CreateClient().GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }

        /// <summary>
        /// 获取poster的byte[]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<byte[]> GetPosterBytes(string id)
        {
            string url = BuildMoviePosterUrl(id);
            logger.LogInformation("准备从MovieDb服务【{url}】获取poster的文件资源bytes", url);

            HttpResponseMessage response = await httpClientFactory.CreateClient().GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }

        public async Task<CodePref?> GetCodePref(string codePrefName)
        {
            string getUrl = BuildCodePrefUrl(codePrefName);
            HttpResponseMessage response = await httpClientFactory.CreateClient().GetAsync(getUrl);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();

            var message = JsonSerializer.Deserialize<ApiResponseMessage<CodePref?>>(
                jsonResponse, JsonApiFormat.CASE_INSENSITIVE_OPTIONS)!;

            return message.Data;
        }
    }
}