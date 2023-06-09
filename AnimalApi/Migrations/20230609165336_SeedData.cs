using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalApi.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    AnimalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Species = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Breed = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AdoptionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "AdoptionDate", "Age", "Breed", "Name", "Species" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Selberian Husky", "Tiger", "Dog" },
                    { 2, new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Bulldog", "Rexie", "Dog" },
                    { 3, new DateTime(2023, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Goldern Retriever", "Matilda", "Dog" },
                    { 4, new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "British Shorthair", "Pip", "Cat" },
                    { 5, new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Ragdoll", "Bartholomew", "Cat" },
                    { 6, new DateTime(2023, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Birman", "Jasmine", "Cat" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
