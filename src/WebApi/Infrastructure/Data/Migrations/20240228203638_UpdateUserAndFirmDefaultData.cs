using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class UpdateUserAndFirmDefaultData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "Firms",
            keyColumn: "Id",
            keyValue: 1,
            column: "GuidIdentifier",
            value: new Guid("2f477197-1004-41c7-9c45-a8015935c439"));

        migrationBuilder.UpdateData(
            table: "Firms",
            keyColumn: "Id",
            keyValue: 2,
            column: "GuidIdentifier",
            value: new Guid("d720527f-2dc5-44b3-a7c4-0d46f9fb865b"));

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 1,
            column: "RegistrationDate",
            value: new DateTime(2024, 2, 28, 4, 18, 10, 0, DateTimeKind.Utc));

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 2,
            column: "RegistrationDate",
            value: new DateTime(2024, 2, 28, 4, 18, 10, 0, DateTimeKind.Utc));

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 3,
            column: "RegistrationDate",
            value: new DateTime(2024, 2, 28, 4, 18, 10, 0, DateTimeKind.Utc));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "Firms",
            keyColumn: "Id",
            keyValue: 1,
            column: "GuidIdentifier",
            value: new Guid("0babfb2b-19cc-4538-bbaf-77c6f2fbfbb4"));

        migrationBuilder.UpdateData(
            table: "Firms",
            keyColumn: "Id",
            keyValue: 2,
            column: "GuidIdentifier",
            value: new Guid("bcc332b6-b268-49d5-a6ab-e29964e7a850"));

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 1,
            column: "RegistrationDate",
            value: new DateTime(2024, 2, 21, 17, 28, 46, 446, DateTimeKind.Local).AddTicks(5424));

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 2,
            column: "RegistrationDate",
            value: new DateTime(2024, 2, 21, 17, 28, 46, 446, DateTimeKind.Local).AddTicks(5442));

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 3,
            column: "RegistrationDate",
            value: new DateTime(2024, 2, 21, 17, 28, 46, 446, DateTimeKind.Local).AddTicks(5444));
    }
}