using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Dtos.Review;
using PokeApi.Interfaces;
using PokeApi.Models;

namespace PokeApi.Controllers
{
    [Route("/api/reviews")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            this._reviewRepository = reviewRepository;
            this._mapper = mapper;
        }

        [HttpGet("", Name = "GetReviews")]
        [ProducesResponseType(200, Type = typeof(ICollection<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = this._mapper.Map<List<ReviewDto>>(this._reviewRepository.GetReviews());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return Ok(reviews);
        }

        [HttpGet("review/{id}", Name = "GetReviewById")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetReviewById(string id)
        {
            if (!this._reviewRepository.ReviewExist(id))
            {
                return NotFound();
            }

            var review = this._mapper.Map<ReviewDto>(this._reviewRepository.GetReview(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return Ok(review);
        }

        [HttpGet("" +
            "pokemon/{pokemonId}", Name = "GetReviewsByPokemonId")]
        [ProducesResponseType(200, Type = typeof(List<ReviewDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByPokemonId(string pokemonId)
        {
            if (!this._reviewRepository.PokemonExist(pokemonId))
            {
                return NotFound();
            }

            var review = this._mapper.Map<List<ReviewDto>>(this._reviewRepository.GetReviewsByPokemon(pokemonId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            };

            return Ok(review);
        }
    }
}
