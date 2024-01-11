using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokeApi.Migrations
{
    public partial class UpdateColumnsAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Trainers_TrainerId",
                table: "Pokemons");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Types",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Types",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Types",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Trainers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PokemonTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PokemonTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PokemonTypes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "Pokemons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Pokemons",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Pokemons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pokemons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pokemons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PokemonAbilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "PokemonAbilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PokemonAbilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Abilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Abilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Abilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Trainers_TrainerId",
                table: "Pokemons",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pokemons_Trainers_TrainerId",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Types");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PokemonAbilities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Abilities");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Trainers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrainerId",
                table: "Pokemons",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                table: "Pokemons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pokemons_Trainers_TrainerId",
                table: "Pokemons",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "Id");
        }
    }
}
