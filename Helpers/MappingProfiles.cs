using AutoMapper;
using PokeApi.Dtos;
using PokeApi.Models;

namespace PokeApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<Models.Type, TypeDto>();
            CreateMap<Trainer, TrainerDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Ability, AbilityDto>();
        }
    }
}
