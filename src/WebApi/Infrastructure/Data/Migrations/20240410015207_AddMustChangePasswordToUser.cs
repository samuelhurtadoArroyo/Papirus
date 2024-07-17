﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class AddMustChangePasswordToUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "MustChangePassword",
            table: "Users",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 1,
            column: "MustChangePassword",
            value: false);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 2,
            column: "MustChangePassword",
            value: false);

        migrationBuilder.UpdateData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 3,
            column: "MustChangePassword",
            value: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "MustChangePassword",
            table: "Users");
    }
}