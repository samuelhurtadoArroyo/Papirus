namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("Status");

        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new Status { Id = 1, Name = "Pendiente" },
            new Status { Id = 2, Name = "Asignado" },
            new Status { Id = 3, Name = "En Proceso" },
            new Status { Id = 4, Name = "Terminado" },
            new Status { Id = 5, Name = "Cerrado" }
        );
    }
}