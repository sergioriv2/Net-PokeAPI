using Microsoft.EntityFrameworkCore;
using PokeApi.Models;

namespace PokeApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet <Pokemon> Pokemons { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<PokemonAbility> PokemonAbilities { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public  DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Pokemon Abilities  ------------------
            modelBuilder.Entity<PokemonAbility>()
                .HasKey(pa => new { pa.PokemonId, pa.AbilityId });

            modelBuilder.Entity<PokemonAbility>()
                .HasOne(p => p.Pokemon)
                .WithMany(p => p.PokemonAbilities)
                .HasForeignKey(pa => pa.PokemonId);

            modelBuilder.Entity<PokemonAbility>()
               .HasOne(p => p.Ability)
               .WithMany(a => a.PokemonAbilities)
               .HasForeignKey(pa => pa.AbilityId);

            // Pokemon Types ------------------
            modelBuilder.Entity<PokemonType>()
               .HasKey(pt => new { pt.TypeId, pt.PokemonId });

            modelBuilder.Entity<PokemonType>()
               .HasOne(p => p.Type)
               .WithMany(t => t.PokemonTypes)
               .HasForeignKey(pt => pt.TypeId);

            modelBuilder.Entity<PokemonType>()
               .HasOne(p => p.Pokemon)
               .WithMany(p => p.PokemonTypes)
               .HasForeignKey(pt => pt.PokemonId);
        }
    }
}
