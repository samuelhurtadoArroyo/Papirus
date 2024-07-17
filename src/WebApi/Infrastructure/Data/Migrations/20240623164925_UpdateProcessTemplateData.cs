using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class UpdateProcessTemplateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: ["ProcessId", "ProcessTypeId", "SubProcessId"],
                values: [8, 2, null]);

            migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: ["ProcessId", "ProcessTypeId", "SubProcessId"],
                values: [8, 2, null]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 1,
                columns: ["ProcessId", "ProcessTypeId", "SubProcessId"],
                values: [1, 1, 1]);

            migrationBuilder.UpdateData(
                table: "ProcessTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: ["ProcessId", "ProcessTypeId", "SubProcessId"],
                values: [1, 1, 1]);
        }
    }
}