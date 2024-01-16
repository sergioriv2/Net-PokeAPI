using AutoMapper;
using PokeApi.Dtos.Pokemon;
using PokeApi.Models;

namespace PokeApi.Helpers
{
    public class PokemonProfiles : Profile
    {
        public PokemonProfiles()
        {
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<CreatePokemonDto, Pokemon>();
        }
    }
}
