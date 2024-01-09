using PokeApi.Models;

namespace PokeApi.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(string id);
        ICollection<Review> GetReviewsByPokemon(string pokemonId);
        bool ReviewExist (string id);
        bool PokemonExist(string pokemonId);

    }
}
