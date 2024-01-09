using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            this._pokemonRepository = pokemonRepository;
            this._mapper = mapper;
        }

        [HttpGet("", Name ="GetPokemons")]
        [ProducesResponseType(200, Type = typeof(ICollection<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pokemons);
        }

        [HttpGet("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPokemon(string pokemonId)
        {
            if (!this._pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound(ModelState);
            }

            var pokemon =  _mapper.Map<PokemonDto>(this._pokemonRepository.GetPokemon(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(pokemon);
        }
    }
}
