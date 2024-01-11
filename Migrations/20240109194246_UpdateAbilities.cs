using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeApi.Migrations
{
    public partial class UpdateAbilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_PokemonAbilities_AbilityId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_Pokemons_PokemonId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pokemons_PokemonId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_AbilityId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "AbilityId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "LearnedAt",
                table: "Abilities");

            migrationBuilder.AlterColumn<string>(
                name: "PokemonId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PokemonAbilities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PokemonId",
                table: "PokemonAbilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AbilityId",
                table: "PokemonAbilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LearnedAt",
                table: "PokemonAbilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Abilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Abilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Abilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Abilities_AbilityId",
                table: "PokemonAbilities",
                column: "AbilityId",
                principalTable: "Abilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonAbilities_Pokemons_PokemonId",
                table: "PokemonAbilities",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pokemons_PokemonId",
                table: "Reviews",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Abilities_AbilityId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonAbilities_Pokemons_PokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pokemons_PokemonId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities");

            migrationBuilder.DropIndex(
                name: "IX_PokemonAbilities_AbilityId",
                table: "PokemonAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "AbilityId",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "LearnedAt",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Abilities");

            migrationBuilder.AlterColumn<string>(
                name: "PokemonId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PokemonAbilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PokemonAbilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PokemonAbilities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Abilities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PokemonId",
                table: "Abilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AbilityId",
                table: "Abilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LearnedAt",
                table: "Abilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonAbilities",
                table: "PokemonAbilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abilities",
                table: "Abilities",
                columns: new[] { "PokemonId", "AbilityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_AbilityId",
                table: "Abilities",
                column: "AbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_PokemonAbilities_AbilityId",
                table: "Abilities",
                column: "AbilityId",
                principalTable: "PokemonAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_Pokemons_PokemonId",
                table: "Abilities",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pokemons_PokemonId",
                table: "Reviews",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id");
        }
    }
}
