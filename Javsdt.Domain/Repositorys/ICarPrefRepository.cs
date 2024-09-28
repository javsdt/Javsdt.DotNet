using Javsdt.Shared.Enums;

namespace Javsdt.Domain.Repositorys
{
    public interface ICarPrefRepository
    {
        JavType GetJavType(string carPrefName);
    }
}
