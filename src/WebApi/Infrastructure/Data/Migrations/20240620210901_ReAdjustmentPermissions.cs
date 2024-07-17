using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Papirus.WebApi.Infrastructure.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ReAdjustmentPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 19, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 20, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 21, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 22, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 25, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 26, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 27, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 28, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 29, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 30, 2 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52,
                column: "PermissionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53,
                column: "PermissionId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54,
                column: "PermissionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55,
                column: "PermissionId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56,
                column: "PermissionId",
                value: 18);

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 57, 19, 3 },
                    { 58, 20, 3 },
                    { 59, 21, 3 },
                    { 60, 22, 3 },
                    { 61, 25, 3 },
                    { 62, 26, 3 },
                    { 63, 27, 3 },
                    { 64, 28, 3 },
                    { 65, 29, 3 },
                    { 66, 30, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 14, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 16, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 17, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 18, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 19, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 20, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 21, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 22, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 25, 3 });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 52,
                column: "PermissionId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 53,
                column: "PermissionId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 54,
                column: "PermissionId",
                value: 28);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 55,
                column: "PermissionId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 56,
                column: "PermissionId",
                value: 30);
        }
    }
}