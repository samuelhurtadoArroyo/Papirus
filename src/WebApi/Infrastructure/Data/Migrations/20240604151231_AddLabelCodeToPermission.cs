using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class AddLabelCodeToPermission : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "PermissionLabelCode",
            table: "Permissions",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.UpdateData(
            table: "Permissions",
            keyColumn: "Id",
            keyValue: 1,
            column: "PermissionLabelCode",
            value: "dashboard_administracion_demandas");

        migrationBuilder.UpdateData(
            table: "Permissions",
            keyColumn: "Id",
            keyValue: 2,
            column: "PermissionLabelCode",
            value: "dashboard_administracion_tutelas");

        migrationBuilder.UpdateData(
            table: "Permissions",
            keyColumn: "Id",
            keyValue: 3,
            column: "PermissionLabelCode",
            value: "dashboard_administracion_configuracion");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PermissionLabelCode",
            table: "Permissions");
    }
}