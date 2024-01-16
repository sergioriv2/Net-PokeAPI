using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos.FileUpload;
using PokeApi.Dtos.Pokemon;
using PokeApi.Interfaces;
using PokeApi.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PokeApi.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    [Authorize]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, ITrainerRepository trainerRepository, IMapper mapper)
        {
            this._pokemonRepository = pokemonRepository;
            this._trainerRepository = trainerRepository;
            this._mapper = mapper;
        }

        [HttpGet("", Name = "GetPokemons")]
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

            var pokemon = _mapper.Map<PokemonDto>(await _pokemonRepository.GetPokemon(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PokemonDto))]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePokemon(
            [FromBody] CreatePokemonDto payload
        )
        {
            var username = User.FindFirst("username")?.Value;

            if (payload == null)
            {
                return BadRequest(ModelState);
            }

            var trainerEntity = _trainerRepository.GetTrainerByUsername(username);
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


            payload.Birthdate = payload.Birthdate == null ? DateTime.UtcNow : payload.Birthdate;
            var pokemonMapped = _mapper.Map<Pokemon>(payload);
            pokemonMapped.TrainerId = trainerEntity.Id;

            var PokemonCreated = await _pokemonRepository.CreatePokemon(pokemonMapped, payload.TypeId);
            if (PokemonCreated == null) 
            {
                ModelState.AddModelError("", "Error at Saving Pokemon");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            var PokemonResponseMapped = _mapper.Map<PokemonDto>(PokemonCreated);

            return StatusCode(StatusCodes.Status201Created, PokemonResponseMapped);
        }

        [HttpPatch("pokemon/{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(PokemonDto))]
        public async Task<IActionResult> UpdatePokemonImage(
              [FromForm] FileUploadDto payload,
              string pokemonId
            )
        {
            try
            {
                var username = User.FindFirst("username")?.Value;
                if (payload == null)
                {
                    return BadRequest(ModelState);
                }

                if (!await _pokemonRepository.PokemonExists(pokemonId))
                {
                    return NotFound();
                }

                var pokemonEntity = await _pokemonRepository.GetPokemon(pokemonId);
                var updatedPokemon = await _pokemonRepository.UpdatePokemonImage(pokemonEntity, payload);

                var mappedPokemon = _mapper.Map<PokemonDto>(updatedPokemon);

                return Ok(mappedPokemon);

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return Ok();
            }
        }
    }
}
