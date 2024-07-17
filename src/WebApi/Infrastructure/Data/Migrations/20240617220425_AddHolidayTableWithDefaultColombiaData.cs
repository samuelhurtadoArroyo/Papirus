using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    /// <inheritdoc />
    public partial class AddHolidayTableWithDefaultColombiaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var createTableBuilder = migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                        Date = table.Column<DateTime>(type:"datetime"),
                        Description = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Holidays", x => x.Id));

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: ["Id", "Description", "Date"],
                values: new object[,]
                {
                    { 1, "Feast of St Peter and St Paul", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Independence Day", new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Battle of Boyacá Day", new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Assumption Day Holiday", new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Columbus Day Holiday", new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "All Saints Day Holiday", new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "Independence of Cartagena Holiday", new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "Immaculate Conception", new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "Christmas Day", new DateTime(2024, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Holidays");
        }
    }
}
