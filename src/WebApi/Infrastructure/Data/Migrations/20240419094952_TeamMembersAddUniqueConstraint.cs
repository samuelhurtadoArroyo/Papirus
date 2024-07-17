using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Papirus.WebApi.Infrastructure.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class TeamMembersAddUniqueConstraint : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddUniqueConstraint(
            name: "TeamMembersUniqueConstraint",
            table: "TeamMembers",
            columns: ["TeamId", "MemberId"]
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropUniqueConstraint(
            name: "TeamMembersUniqueConstraint",
            table: "TeamMembers"
        );
    }
}