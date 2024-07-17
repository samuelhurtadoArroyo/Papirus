namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new Role { Id = 1, Name = "Super Administrador" },
            new Role { Id = 2, Name = "Líder Tutelas" },
            new Role { Id = 3, Name = "Asistente Tutelas" }
        );
    }
}