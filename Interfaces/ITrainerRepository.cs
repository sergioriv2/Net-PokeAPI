using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface ITrainerRepository
    {
        ICollection<Trainer> GetTrainers();
        Trainer GetTrainer(string id);
        Trainer GetTrainerByUsername(string username);
        bool TrainerExists(string id);
    }
}
