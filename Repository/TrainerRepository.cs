using Microsoft.EntityFrameworkCore;
using PokeApi.Data;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Repository
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly DataContext _context;
        public TrainerRepository(DataContext dataContext)
        {
            this._context = dataContext;
        }
        public async Task<Trainer> CreateTrainer(Trainer trainer)
        {
            trainer.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(trainer.Password);
            await _context.AddAsync(trainer);
            await Save();
            return trainer;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<RefreshTokens> AddUserRefreshToken(RefreshTokens refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await Save();
            return refreshToken;
        }

        public async Task DeleteUserRefreshToken(string trainerId, string refreshToken)
        {
            var token = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.TrainerId == trainerId && r.RefreshToken == refreshToken);
            if (token != null)
            {
                _context.RefreshTokens.Remove(token);
                await Save();
                return;
            }
            return;
        }

        public async Task<RefreshTokens> GetSavedRefreshToken(string trainerId, string refreshtoken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(r => r.TrainerId == trainerId && r.RefreshToken == refreshtoken);
        }

        public Trainer GetTrainer(string id)
        {
            return this._context.Trainers.Where(t => t.Id == id).FirstOrDefault();
        }

        public Trainer GetTrainerByUsername(string username)
        {
            return this._context.Trainers.Where(t => t.Username == username).FirstOrDefault();
        }

        public ICollection<Trainer> GetTrainers()
        {
            return this._context.Trainers.ToList();
        }

        public bool TrainerExists(string id)
        {
            return this._context.Trainers.Any(t => t.Id == id);
        }

        public Task<bool> TrainerExists(Trainer trainer)
        {
            return this._context.Trainers.AnyAsync(t => t.Username == trainer.Username || t.Id == trainer.Id);
        }

        public bool VerifyCredentials(Trainer trainer, string passwordToVerify)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(passwordToVerify, trainer.Password);
        }
    }
}
