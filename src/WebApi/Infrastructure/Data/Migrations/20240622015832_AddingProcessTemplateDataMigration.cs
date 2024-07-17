using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class AddingProcessTemplateDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProcessTemplates",
                columns: new[] { "Id", "FileName", "FilePath", "FirmId", "ProcessId", "ProcessTypeId", "SubProcessId" },
                values: new object[,]
                {
                    { 1, "PLANTILLA 1. CONTESTACION SENCILLA TUTELA.docx", "Templates\\Guardianships", 1, 1, 1, 1 },
                    { 2, "PLANTILLA 2. ESCRITO DE EMERGENCIA.docx", "Templates\\Guardianships", 1, 1, 1, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
