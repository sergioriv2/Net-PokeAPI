using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos.Pokemon;
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
        public async Task<IActionResult> GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(await _pokemonRepository.GetPokemons());

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
        public async Task<IActionResult> GetPokemon(string pokemonId)
        {
            if (!await _pokemonRepository.PokemonExists(pokemonId))
            {
                return NotFound();
            }

            var pokemon =  _mapper.Map<PokemonDto>(await _pokemonRepository.GetPokemon(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePokemon(
            [FromQuery] string typeId,
            [FromQuery] string trainerId,
            [FromBody] PokemonDto payload
        )
        {
            if (payload == null)
            {
                return BadRequest(ModelState);
            }

            var pokemonsByName = await _pokemonRepository.GetPokemonByName(payload.Name);

            if (pokemonsByName != null)
            {
                ModelState.AddModelError("", "Pokemon Already Exist");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }

            var pokemonMapped = _mapper.Map<Pokemon>(payload);

            if (!await _pokemonRepository.CreatePokemon(typeId, trainerId, pokemonMapped))
            {
                ModelState.AddModelError("", "Error at Saving Pokemon");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
