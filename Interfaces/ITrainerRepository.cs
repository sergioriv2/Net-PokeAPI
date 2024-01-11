using PokeApi.Dtos.Auth;
using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface ITrainerRepository
    {
        ICollection<Trainer> GetTrainers();
        Trainer GetTrainer(string id);
        Trainer GetTrainerByUsername(string username);
        Task<Trainer> CreateTrainer(Trainer trainer);
        bool TrainerExists(string id);
        bool VerifyCredentials(Trainer trainer, string passwordToVerify);
        Task<bool> TrainerExists(Trainer trainer);
        Task<RefreshTokens> AddUserRefreshToken(RefreshTokens refreshToken);
        Task<RefreshTokens> GetSavedRefreshToken(string username, string refreshtoken);
        Task DeleteUserRefreshToken(string username, string refreshToken);
    }
}
