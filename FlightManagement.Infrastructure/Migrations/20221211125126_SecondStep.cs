using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3abb77c4-5f48-4e56-853e-74b24d1c711e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("584849e3-9ca3-4962-847d-38ce69e0f138"));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Countries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Countries");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("584849e3-9ca3-4962-847d-38ce69e0f138"), "Usa" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("3abb77c4-5f48-4e56-853e-74b24d1c711e"), new Guid("584849e3-9ca3-4962-847d-38ce69e0f138"), "New York" });
        }
    }
}
