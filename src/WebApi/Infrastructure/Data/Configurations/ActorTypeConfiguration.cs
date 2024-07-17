namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class ActorTypeConfiguration : IEntityTypeConfiguration<ActorType>
{
    public void Configure(EntityTypeBuilder<ActorType> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new ActorType { Id = 1, Name = "Reclamante" },
            new ActorType { Id = 2, Name = "Defendido" }
        );
    }
}