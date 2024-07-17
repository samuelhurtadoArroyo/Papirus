using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class Addednewholidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "New year's day" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Epiphany" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Saint Joseph's day" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Maundy Thursday" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Great Friday" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Labor day" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Ascension day" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Corpus Christi" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Sacred Heart" });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "Date", "Description" },
                values: new object[,]
                {
                    { 10, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "St Peter and Saint Paul Day" },
                    { 11, new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Independence Day" },
                    { 12, new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Battle of Boyacá Day" },
                    { 13, new DateTime(2024, 8, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Assumption of mary" },
                    { 14, new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Columbus Day" },
                    { 15, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), "All Saints Day" },
                    { 16, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Independence of Cartagena" },
                    { 17, new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Immaculate Conception" },
                    { 18, new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Christmas Day" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Feast of St Peter and St Paul" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Independence Day" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Battle of Boyacá Day" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Assumption Day Holiday" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Columbus Day Holiday" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), "All Saints Day Holiday" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Independence of Cartagena Holiday" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Immaculate Conception" });

            migrationBuilder.UpdateData(
                table: "Holidays",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Date", "Description" },
                values: new object[] { new DateTime(2024, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Christmas Day" });
        }
    }
}
