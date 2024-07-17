using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public partial class DeleteEmailfromPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_People_Email",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "People",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "tutelascentro@bancolombia.com.co");

            migrationBuilder.CreateIndex(
                name: "UQ_People_Email",
                table: "People",
                column: "Email",
                unique: true);
        }
    }
}
