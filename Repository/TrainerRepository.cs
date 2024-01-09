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
    }
}
