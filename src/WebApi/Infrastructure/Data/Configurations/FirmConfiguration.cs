namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class FirmConfiguration : IEntityTypeConfiguration<Firm>
{
    public void Configure(EntityTypeBuilder<Firm> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasIndex(e => e.GuidIdentifier, "UQ_Firms_GuidIdentifier").IsUnique();

        builder.HasData(
            new Firm { Id = 1, GuidIdentifier = Guid.Parse("2f477197-1004-41c7-9c45-a8015935c439"), Name = "Gómez Pineda Abogados" },
            new Firm { Id = 2, GuidIdentifier = Guid.Parse("d720527f-2dc5-44b3-a7c4-0d46f9fb865b"), Name = "Alianza" }
        );
    }
}