using AutoMapper;
using PokeApi.Dtos.Ability;
using PokeApi.Dtos.Auth;
using PokeApi.Dtos.Pokemon;
using PokeApi.Dtos.PokemonTypes;
using PokeApi.Dtos.Review;
using PokeApi.Dtos.Trainer;
using PokeApi.Models;

namespace PokeApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // PokemonTypes DTOs
            CreateMap<Models.Type, TypeDto>();

            // Review DTOs
            CreateMap<Review, ReviewDto>();

            // Ability DTOs
            CreateMap<Ability, AbilityDto>();
        }
    }
}
