using PokeApi.Data;
using PokeApi.Models;
using System.Diagnostics.Metrics;

namespace PokeApi
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Trainers.Any())
            {
                var pokemonTrainers = new List<Trainer>()
                {
                    new Trainer()
                    {
                        Pokemons = new List<Pokemon>()
                        {
                            new Pokemon()
                            {
                                    Id = Guid.NewGuid().ToString(),
                                    Name = "Pikachu",
                                    Birthdate = new DateTime(1903,1,1),
                                    PokemonTypes = new List<PokemonType>()
                                      {
                                        new PokemonType { Type = new Models.Type() {  Id = Guid.NewGuid().ToString(), Name = "Electric" }}
                                    },
                                    Reviews = new List<Review>()
                                    {
                                        new Review {  Id = Guid.NewGuid().ToString(), Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric" },
                                    }
                            }

                        },
                            FirstName = "Jack",
                            LastName = "London",
                            Username = "JackLondon20",
                            Password = "Jack",
                             Id = Guid.NewGuid().ToString()
                    },
                    new Trainer()
                    {
                        Pokemons = new List<Pokemon>()
                            {
                                 new Pokemon()
                                {Id = Guid.NewGuid().ToString(),
                                Name = "Squirtle",
                                Birthdate = new DateTime(1903, 1, 1),
                                PokemonTypes = new List<PokemonType>()
                                {
                                    new PokemonType { Type = new Models.Type() {  Id = Guid.NewGuid().ToString(), Name = "Water"}}
                                },
                                Reviews = new List<Review>()
                                {
                                    new Review {  Id = Guid.NewGuid().ToString(), Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric" },
                                    new Review {  Id = Guid.NewGuid().ToString(), Title= "Squirtle",Text = "Squirtle is the best a killing rocks" },
                                    new Review {  Id = Guid.NewGuid().ToString(), Title= "Squirtle", Text = "squirtle, squirtle, squirtle"},
                                }
                                }
                            },
                        FirstName = "Harry",
                        LastName = "Potter",
                        Username = "HarryPotter",
                        Password = "Potter",
                        Id = Guid.NewGuid().ToString()
                    },
                    new Trainer()
                    {
                        Pokemons = new List<Pokemon>()
                        {
                            new Pokemon()
                            {Id = Guid.NewGuid().ToString(),
                                            Name = "Venasuar",
                            Birthdate = new DateTime(1903, 1, 1),
                            PokemonTypes = new List<PokemonType>()
                                {
                                    new PokemonType { Type = new Models.Type() {Id = Guid.NewGuid().ToString(),  Name = "Leaf"}}
                                },
                            Reviews = new List<Review>()
                                {
                                    new Review {Id = Guid.NewGuid().ToString(),  Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric"},
                                    new Review {Id = Guid.NewGuid().ToString(),  Title="Veasaur",Text = "Venasuar is the best a killing rocks" },
                                    new Review {Id = Guid.NewGuid().ToString(),  Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar" },
                                }
                            }

                        },
                        FirstName = "Ash",
                        LastName = "Ketchum",
                        Username = "AshKetchum",
                        Password = "Pikachu",
                        Id = Guid.NewGuid().ToString()
                    }
                };
                
                dataContext.Trainers.AddRange(pokemonTrainers);
                dataContext.SaveChanges();
            }
        }
    }
}

