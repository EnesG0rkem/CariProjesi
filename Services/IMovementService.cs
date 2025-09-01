using System.Threading.Tasks;
using CariProjesi.Models;

namespace CariProjesi.Services
{
    public interface IMovementService : IService<Movement>
    {
        Task<string?> FindAccountNameAsync(string accountCode);
    }
}
