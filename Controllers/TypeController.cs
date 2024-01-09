using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos;
using PokeApi.Interfaces;

namespace PokeApi.Controllers
{
    [Route("api/types")]
    [ApiController]
    public class TypeController : Controller
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TypeController(ITypeRepository typeRepository, IMapper mapper)
        {
            this._typeRepository = typeRepository;
            this._mapper = mapper;
        }

        [HttpGet("", Name = "GetTypes")]
        [ProducesResponseType(200, Type = typeof(ICollection<Models.Type>))]
        [ProducesResponseType(400)]
        public IActionResult GetTypes()
        {
            var types = _mapper.Map<List<TypeDto>>(this._typeRepository.GetTypes());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(types);
        }

        [HttpGet("type/{typeId}", Name = "GetTypeById")]
        [ProducesResponseType(200, Type = typeof(Models.Type))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetType(string id)
        {

            if (!this._typeRepository.TypeExists(id))
            {
                return NotFound(ModelState);
            }

            var types = this._mapper.Map<TypeDto>(this._typeRepository.GetType());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(types);
        }

        [HttpGet("pokemon/{typeId}", Name = "GetPokemonByTypeId")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Type>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPokemonByTypeId(string typeId)
        {

            if (!this._typeRepository.TypeExists(typeId))
            {
                return NotFound();
            }

            var types = this._mapper.Map<List<PokemonDto>>(this._typeRepository.GetPokemonsByType(typeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(types);
        }
    }
}
