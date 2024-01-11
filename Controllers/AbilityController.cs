using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos.Ability;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Controllers
{
    [Route("/api/abilities")]
    [ApiController]
    public class AbilityController : Controller
    {
        private readonly IAbilityRepository _abilityRepository;
        private readonly IMapper _mapper;

        public AbilityController(IAbilityRepository abilityRepository, IMapper mapper)
        {
            this._abilityRepository = abilityRepository;
            this._mapper = mapper;
        }

        [HttpGet("", Name = "GetAbilities")]
        [ProducesResponseType(200, Type = typeof(ICollection<Ability>))]
        [ProducesResponseType(400)]
        public IActionResult GetAbilities()
        {
            var abilities = this._mapper.Map<List<AbilityDto>>(_abilityRepository.GetAbilities());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(abilities);
        }

        [HttpGet("pokemon/{pokemonId}", Name = "GetPokemonAbilities")]
        [ProducesResponseType(200, Type = typeof(ICollection<Ability>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonAbilities(string pokemonId)
        {
            if (!this._abilityRepository.PokemonExist(pokemonId))
            {
                return NotFound();
            }

            var abilities = this._mapper.Map<List<AbilityDto>>(_abilityRepository.GetPokemonAbilities(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(abilities);
        }

        [HttpGet("ability/{id}", Name = "GetAbility")]
        [ProducesResponseType(200, Type = typeof(ICollection<Ability>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetAbility(string id)
        {
            if (!this._abilityRepository.AbilityExists(id))
            {
                return NotFound();
            }

            var ability = this._mapper.Map<AbilityDto>(_abilityRepository.GetAbility(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(ability);
        }

    }
}
