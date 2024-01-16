using AutoMapper;
using PokeApi.Dtos.Auth;
using PokeApi.Dtos.Trainer;
using PokeApi.Models;

namespace PokeApi.Helpers
{
    public class TrainerProfiles : Profile
    {
        public TrainerProfiles()
        {
            CreateMap<TrainerSignupDto, Trainer>();
            CreateMap<TrainerLoginDto, Trainer>();
            CreateMap<Trainer, TrainerDto>();
        }
    }
}
