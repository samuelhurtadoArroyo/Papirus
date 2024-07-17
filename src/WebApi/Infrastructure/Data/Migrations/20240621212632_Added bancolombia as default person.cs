using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class Addedbancolombiaasdefaultperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Email", "GuidIdentifier", "IdentificationNumber", "IdentificationTypeId", "Name", "PersonTypeId", "SupportFileName", "SupportFilePath" },
                values: new object[] { 1, "tutelascentro@bancolombia.com.co", new Guid("4f9d0b04-8031-4b89-9a9d-3f8f90c0f8ec"), "890903938-8", 1, "Bancolombia S.A.", 1, "Archivo de Soporte Bancolombia.pdf", "path/bancolombia1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
