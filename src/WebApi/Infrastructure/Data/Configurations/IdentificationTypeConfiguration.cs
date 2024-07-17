namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType>
{
    public void Configure(EntityTypeBuilder<IdentificationType> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.Abbreviation).HasMaxLength(50);

        builder.HasData(
            new IdentificationType { Id = 1, Name = "Cédula de Ciudadanía", Abbreviation = "CC" },
            new IdentificationType { Id = 2, Name = "Cédula de Extranjería", Abbreviation = "CE" },
            new IdentificationType { Id = 3, Name = "Pasaporte", Abbreviation = "PT" },
            new IdentificationType { Id = 4, Name = "Número de Identificación Tributaria", Abbreviation = "NIT" },
            new IdentificationType { Id = 5, Name = "Registro Único Tributario", Abbreviation = "RUT" }
        );
    }
}