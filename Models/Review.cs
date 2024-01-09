﻿namespace PokeApi.Models
{
    public class Review
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
    }
}
