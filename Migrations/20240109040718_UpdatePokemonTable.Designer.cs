﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokeApi.Data;

#nullable disable

namespace PokeApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240109040718_UpdatePokemonTable")]
    partial class UpdatePokemonTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PokeApi.Models.Ability", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemonAbilities");
                });

            modelBuilder.Entity("PokeApi.Models.Pokemon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrainerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("PokeApi.Models.PokemonAbility", b =>
                {
                    b.Property<string>("PokemonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AbilityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LearnedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PokemonId", "AbilityId");

                    b.HasIndex("AbilityId");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("PokeApi.Models.PokemonType", b =>
                {
                    b.Property<string>("TypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PokemonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rarity")
                        .HasColumnType("real");

                    b.HasKey("TypeId", "PokemonId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonTypes");
                });

            modelBuilder.Entity("PokeApi.Models.Review", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PokemonId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("PokeApi.Models.Trainer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("PokeApi.Models.Type", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("PokeApi.Models.Pokemon", b =>
                {
                    b.HasOne("PokeApi.Models.Trainer", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("TrainerId");
                });

            modelBuilder.Entity("PokeApi.Models.PokemonAbility", b =>
                {
                    b.HasOne("PokeApi.Models.Ability", "Ability")
                        .WithMany("PokemonAbilities")
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokeApi.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonAbilities")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("PokeApi.Models.PokemonType", b =>
                {
                    b.HasOne("PokeApi.Models.Pokemon", "Pokemon")
                        .WithMany("PokemonTypes")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokeApi.Models.Type", "Type")
                        .WithMany("PokemonTypes")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pokemon");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("PokeApi.Models.Review", b =>
                {
                    b.HasOne("PokeApi.Models.Pokemon", null)
                        .WithMany("Reviews")
                        .HasForeignKey("PokemonId");
                });

            modelBuilder.Entity("PokeApi.Models.Ability", b =>
                {
                    b.Navigation("PokemonAbilities");
                });

            modelBuilder.Entity("PokeApi.Models.Pokemon", b =>
                {
                    b.Navigation("PokemonAbilities");

                    b.Navigation("PokemonTypes");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PokeApi.Models.Trainer", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("PokeApi.Models.Type", b =>
                {
                    b.Navigation("PokemonTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
