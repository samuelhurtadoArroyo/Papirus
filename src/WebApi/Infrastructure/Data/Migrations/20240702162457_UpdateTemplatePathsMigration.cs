using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class UpdateTemplatePathsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FilePath" },
               values: new object[] { "https://salumusdeveu.blob.core.windows.net/papirus/Papirus.Dev/GomezPineda/Tutelas/Templates/PLANTILLA%201.%20CONTESTACION%20SENCILLA%20TUTELA.docx?sv=2024-05-04&st=2024-07-02T19%3A53%3A23Z&se=2025-07-02T19%3A53%3A23Z&sr=b&sp=r&sig=q99FBwqKVQQUYcA6dAltM3wjq9lxLRgerwp4hYaRsEA%3D" });


               migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FilePath" },
               values: new object[] { "https://salumusdeveu.blob.core.windows.net/papirus/Papirus.Dev/GomezPineda/Tutelas/Templates/PLANTILLA%202.%20ESCRITO%20DE%20EMERGENCIA.docx?sv=2024-05-04&st=2024-07-02T19%3A54%3A09Z&se=2025-07-02T19%3A54%3A09Z&sr=b&sp=r&sig=CLt8ygAahQ7r7d3%2F8n2eoUnNUZF5eCBsZ3ziwinSr%2FM%3D" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FilePath" },
               values: new object[] { "Templates\\Guardianships" });


            migrationBuilder.UpdateData(
             table: "ProcessTemplates",
             keyColumn: "Id",
             keyValue: 2,
             columns: new[] { "FilePath" },
            values: new object[] { "Templates\\Guardianships" });

        }
    }
}
