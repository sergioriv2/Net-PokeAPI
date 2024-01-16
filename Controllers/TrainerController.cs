using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos.Auth;
using PokeApi.Dtos.Trainer;
using PokeApi.Filters.Exceptions.Trainer;
using PokeApi.Interfaces;
using PokeApi.Models;
using PokeApi.Responses;

namespace PokeApi.Controllers
{
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(TrainerExceptionsFilter))]
    [Route("/api/trainers")]
    public class TrainerController : Controller
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IJWTManagerRepository _jwtManagerRepository;
        private readonly IMapper _mapper;
        public TrainerController(ITrainerRepository trainerRepository, IMapper mapper, IJWTManagerRepository jwtManagerRepository)
        {
            this._mapper = mapper;
            this._trainerRepository = trainerRepository;
            _jwtManagerRepository = jwtManagerRepository;

        }

        [AllowAnonymous]
        [HttpPost("auth/sign-in", Name = "SignInTrainer")]
        [ProducesResponseType(200, Type = typeof(APIResponseController<TrainerLoginResponse>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignInTrainer(
                [FromBody] TrainerLoginDto payload
            )
        {
            var trainerMapped = _mapper.Map<TrainerLoginDto, Trainer>(payload);
            var trainerEntity = await _trainerRepository.VerifyCredentials(trainerMapped, payload.Password);
            var tokensGenerated = _jwtManagerRepository.GenerateJWT(payload.Username);

            if (tokensGenerated == null)
            {
                return Unauthorized();
            }

            RefreshTokens tokensEntity = new()
            {
                RefreshToken = tokensGenerated.RefreshToken,
                TrainerId = trainerEntity.Id
            };

            await _trainerRepository.AddUserRefreshToken(tokensEntity);

            return Ok(tokensGenerated);
        }

        [AllowAnonymous]
        [HttpPost("auth/sign-up", Name = "SignUpTrainer")]
        [ProducesResponseType(201, Type = typeof(APIResponseController<TrainerDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignUpTrainer(
                [FromBody]
                TrainerSignupDto payload
            )
        {
            var payloadMapped = _mapper.Map<TrainerSignupDto, Trainer>(payload);

            var trainerEntity = await _trainerRepository.CreateTrainer(payloadMapped);
            var trainerResponse = _mapper.Map<TrainerDto>(trainerEntity);

            return StatusCode(201, trainerResponse);

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
        public async Task<IActionResult> GetTrainerById(string id)
        {
            if (! await this._trainerRepository.TrainerExists(id))
            {
                return NotFound();
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
