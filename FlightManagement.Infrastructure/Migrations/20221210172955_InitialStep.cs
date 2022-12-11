using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("93100563-3a36-4d87-b296-e0434f9c69eb"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c5c70a8e-fa88-452c-83d7-3d767fa30337"));

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("584849e3-9ca3-4962-847d-38ce69e0f138"), "Usa" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("3abb77c4-5f48-4e56-853e-74b24d1c711e"), new Guid("584849e3-9ca3-4962-847d-38ce69e0f138"), "New York" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3abb77c4-5f48-4e56-853e-74b24d1c711e"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("584849e3-9ca3-4962-847d-38ce69e0f138"));

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c5c70a8e-fa88-452c-83d7-3d767fa30337"), "Usa" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[] { new Guid("93100563-3a36-4d87-b296-e0434f9c69eb"), new Guid("c5c70a8e-fa88-452c-83d7-3d767fa30337"), "New York" });
        }
    }
}
