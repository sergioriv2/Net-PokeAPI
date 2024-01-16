using Microsoft.EntityFrameworkCore;
using MimeDetective;
using PokeApi.Data;
using PokeApi.Dtos.FileUpload;
using PokeApi.Interfaces;
using PokeApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PokeApi.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        private readonly IAWSRepository _awsRepository;

        public PokemonRepository(DataContext context, IAWSRepository awsRepository)
        {
            _context = context;
            this._awsRepository = awsRepository;
        }

        public async Task<Pokemon> CreatePokemon(Pokemon pokemon, string typeId)
        {
            var typeEntity = await _context.Types.Where(e => e.Id == typeId).FirstOrDefaultAsync();
            var PokemonTypeEntity = new PokemonType()
            {
                Pokemon = pokemon,
                Type = typeEntity
            };

            await _context.AddAsync(PokemonTypeEntity);
            await _context.AddAsync(pokemon);

            await _context.SaveChangesAsync();

            return pokemon;
        }

        public async Task<Pokemon> GetPokemon(string id)
        {
            return await _context.Pokemons.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Pokemon> GetPokemonByName(string name)
        {
            return await _context.Pokemons.Where(x => x.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemons()
        {
            return await _context.Pokemons.ToListAsync();
        }

        public async Task<bool> PokemonExists(string id)
        {
            return await _context.Pokemons.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<Pokemon> UpdatePokemonImage(Pokemon pokemon, FileUploadDto fileDetails)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    fileDetails.FileData.CopyTo(stream);
                    string bucketKey = $"pokemon-images/{pokemon.Name}.{fileDetails.FileType}";
                    System.Console.WriteLine("Key: " + bucketKey);

                    var fileToUpload = new S3File()
                    {
                        Key = bucketKey,
                        BucketName = "poke-api-v2",
                        InputStream = stream
                    };

                    string imageLocation = await _awsRepository.UploadS3Image(fileToUpload);

                    pokemon.Image = imageLocation;
                    pokemon.UpdatedAt = DateTime.UtcNow;

                    _context.Pokemons.Update(pokemon);
                    await _context.SaveChangesAsync();

                    return pokemon;
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
