﻿namespace PokeApi.Models
{
    public class Trainer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Pokemon> Pokemons { get; set; }
    }
}
