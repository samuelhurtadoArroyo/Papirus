namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
            .HasForeignKey(d => d.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_RolePermissions_Permissions");

        builder.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_RolePermissions_Roles");

        builder.HasData(
            /*Super Admin*/
            new RolePermission { Id = 1, RoleId = 1, PermissionId = 1 },
            new RolePermission { Id = 2, RoleId = 1, PermissionId = 2 },
            new RolePermission { Id = 3, RoleId = 1, PermissionId = 3 },
            new RolePermission { Id = 4, RoleId = 1, PermissionId = 4 },
            new RolePermission { Id = 5, RoleId = 1, PermissionId = 5 },
            new RolePermission { Id = 6, RoleId = 1, PermissionId = 6 },
            new RolePermission { Id = 7, RoleId = 1, PermissionId = 7 },
            new RolePermission { Id = 8, RoleId = 1, PermissionId = 8 },
            new RolePermission { Id = 9, RoleId = 1, PermissionId = 9 },
            new RolePermission { Id = 10, RoleId = 1, PermissionId = 10 },
            new RolePermission { Id = 11, RoleId = 1, PermissionId = 11 },
            new RolePermission { Id = 12, RoleId = 1, PermissionId = 12 },
            new RolePermission { Id = 13, RoleId = 1, PermissionId = 13 },
            new RolePermission { Id = 14, RoleId = 1, PermissionId = 14 },
            new RolePermission { Id = 15, RoleId = 1, PermissionId = 15 },
            new RolePermission { Id = 16, RoleId = 1, PermissionId = 16 },
            new RolePermission { Id = 17, RoleId = 1, PermissionId = 17 },
            new RolePermission { Id = 18, RoleId = 1, PermissionId = 18 },
            new RolePermission { Id = 19, RoleId = 1, PermissionId = 19 },
            new RolePermission { Id = 20, RoleId = 1, PermissionId = 20 },
            new RolePermission { Id = 21, RoleId = 1, PermissionId = 21 },
            new RolePermission { Id = 22, RoleId = 1, PermissionId = 22 },
            new RolePermission { Id = 23, RoleId = 1, PermissionId = 23 },
            new RolePermission { Id = 24, RoleId = 1, PermissionId = 24 },
            new RolePermission { Id = 25, RoleId = 1, PermissionId = 25 },
            new RolePermission { Id = 26, RoleId = 1, PermissionId = 26 },
            new RolePermission { Id = 27, RoleId = 1, PermissionId = 27 },
            new RolePermission { Id = 28, RoleId = 1, PermissionId = 28 },
            new RolePermission { Id = 29, RoleId = 1, PermissionId = 29 },
            new RolePermission { Id = 30, RoleId = 1, PermissionId = 30 },

            /*Lider Tutelas*/
            new RolePermission { Id = 31, RoleId = 2, PermissionId = 1 },
            new RolePermission { Id = 32, RoleId = 2, PermissionId = 8 },
            new RolePermission { Id = 33, RoleId = 2, PermissionId = 9 },
            new RolePermission { Id = 34, RoleId = 2, PermissionId = 10 },
            new RolePermission { Id = 35, RoleId = 2, PermissionId = 11 },
            new RolePermission { Id = 36, RoleId = 2, PermissionId = 12 },
            new RolePermission { Id = 37, RoleId = 2, PermissionId = 13 },
            new RolePermission { Id = 38, RoleId = 2, PermissionId = 14 },
            new RolePermission { Id = 39, RoleId = 2, PermissionId = 15 },
            new RolePermission { Id = 40, RoleId = 2, PermissionId = 16 },
            new RolePermission { Id = 41, RoleId = 2, PermissionId = 18 },
            new RolePermission { Id = 42, RoleId = 2, PermissionId = 19 },
            new RolePermission { Id = 43, RoleId = 2, PermissionId = 20 },
            new RolePermission { Id = 44, RoleId = 2, PermissionId = 21 },
            new RolePermission { Id = 45, RoleId = 2, PermissionId = 22 },
            new RolePermission { Id = 46, RoleId = 2, PermissionId = 25 },
            new RolePermission { Id = 47, RoleId = 2, PermissionId = 26 },
            new RolePermission { Id = 48, RoleId = 2, PermissionId = 27 },
            new RolePermission { Id = 49, RoleId = 2, PermissionId = 28 },
            new RolePermission { Id = 50, RoleId = 2, PermissionId = 29 },
            new RolePermission { Id = 51, RoleId = 2, PermissionId = 30 },

            /* Asistente Tutelas*/
            new RolePermission { Id = 52, RoleId = 3, PermissionId = 1 },
            new RolePermission { Id = 53, RoleId = 3, PermissionId = 14 },
            new RolePermission { Id = 54, RoleId = 3, PermissionId = 16 },
            new RolePermission { Id = 55, RoleId = 3, PermissionId = 17 },
            new RolePermission { Id = 56, RoleId = 3, PermissionId = 18 },
            new RolePermission { Id = 57, RoleId = 3, PermissionId = 19 },
            new RolePermission { Id = 58, RoleId = 3, PermissionId = 20 },
            new RolePermission { Id = 59, RoleId = 3, PermissionId = 21 },
            new RolePermission { Id = 60, RoleId = 3, PermissionId = 22 },
            new RolePermission { Id = 61, RoleId = 3, PermissionId = 25 },
            new RolePermission { Id = 62, RoleId = 3, PermissionId = 26 },
            new RolePermission { Id = 63, RoleId = 3, PermissionId = 27 },
            new RolePermission { Id = 64, RoleId = 3, PermissionId = 28 },
            new RolePermission { Id = 65, RoleId = 3, PermissionId = 29 },
            new RolePermission { Id = 66, RoleId = 3, PermissionId = 30 }
        );
    }
}