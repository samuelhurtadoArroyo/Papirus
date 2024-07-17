namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class PersonTypeConfiguration : IEntityTypeConfiguration<PersonType>
{
    public void Configure(EntityTypeBuilder<PersonType> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new PersonType { Id = 1, Name = "Persona Natural" },
            new PersonType { Id = 2, Name = "Persona Jurídica" }
        );
    }
}