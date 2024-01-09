using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos;
using PokeApi.Interfaces;
using PokeApi.Models;
using PokeApi.Repository;

namespace PokeApi.Controllers
{
    [Route("/api/trainers")]
    [ApiController]
    public class TrainerController : Controller
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;
        public TrainerController(ITrainerRepository trainerRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._trainerRepository = trainerRepository;
        }

        [HttpGet("", Name = "GetTrainers")]
        [ProducesResponseType(200, Type = typeof(ICollection<Trainer>))]
        public IActionResult GetTrainers()
        {
            var trainers = _mapper.Map<List<TrainerDto>>(this._trainerRepository.GetTrainers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(trainers);
        }

        [HttpGet("trainer/{id}", Name = "GetTrainerById")]
        [ProducesResponseType(200, Type = typeof(TrainerDto))]
        public IActionResult GetTrainerById(string id)
        {
            if (!this._trainerRepository.TrainerExists(id))
            {
                return NotFound(ModelState);
            }

            var trainers = _mapper.Map<TrainerDto>(this._trainerRepository.GetTrainer(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(trainers);
        }
    }
}
