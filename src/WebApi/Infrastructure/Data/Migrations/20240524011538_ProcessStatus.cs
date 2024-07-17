using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class ProcessStatus : Migration
{
    private static readonly string[] columns = ["Id", "Name"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ProcessStatusId",
            table: "Cases",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateTable(
            name: "ProcessStatus",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_ProcessStatus", x => x.Id));

        migrationBuilder.InsertData(
            table: "ProcessStatus",
            columns: columns,
            values: new object[,]
            {
                { 1, "Pendiente Asignación" },
                { 2, "Asignada" },
                { 3, "En Progreso" },
                { 4, "Contestada" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Cases_ProcessStatusId",
            table: "Cases",
            column: "ProcessStatusId");

        migrationBuilder.AddForeignKey(
            name: "FK_Cases_ProcessStatus",
            table: "Cases",
            column: "ProcessStatusId",
            principalTable: "ProcessStatus",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Cases_ProcessStatus",
            table: "Cases");

        migrationBuilder.DropTable(
            name: "ProcessStatus");

        migrationBuilder.DropIndex(
            name: "IX_Cases_ProcessStatusId",
            table: "Cases");

        migrationBuilder.DropColumn(
            name: "ProcessStatusId",
            table: "Cases");
    }
}