using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class AddBusinessLinesTable : Migration
{
    private static readonly string[] columns = ["Id", "Name"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "BusinessLineId",
            table: "Cases",
            type: "int",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "BusinessLines",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_BusinessLines", x => x.Id));

        migrationBuilder.InsertData(
            table: "BusinessLines",
            columns: columns,
            values: new object[,]
            {
                { 1, "Bancolombia S.A" },
                { 2, "Leasing" },
                { 3, "Sufi" },
                { 4, "Renting" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Cases_BusinessLineId",
            table: "Cases",
            column: "BusinessLineId");

        migrationBuilder.AddForeignKey(
            name: "FK_Cases_BusinessLines_BusinessLineId",
            table: "Cases",
            column: "BusinessLineId",
            principalTable: "BusinessLines",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Cases_BusinessLines_BusinessLineId",
            table: "Cases");

        migrationBuilder.DropTable(
            name: "BusinessLines");

        migrationBuilder.DropIndex(
            name: "IX_Cases_BusinessLineId",
            table: "Cases");

        migrationBuilder.DropColumn(
            name: "BusinessLineId",
            table: "Cases");
    }
}