using Javsdt.Domain.Entitys;

namespace Javsdt.Domain.Repositorys
{
    public interface IMovieRepository
    {
        /// <summary>
        /// 查询电影详情
        /// </summary>
        /// <param name="carName"></param>
        /// <returns></returns>
        Movie? GetDetail(string carName);

        /// <summary>
        /// 获取fanart字节流
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        Task<byte[]> GetFanartBytes(string car);

        /// <summary>
        /// 获取poster字节流
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        Task<byte[]> GetPosterBytes(string car);
    }
}