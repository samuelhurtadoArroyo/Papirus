namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class BusinessLineConfiguration : IEntityTypeConfiguration<BusinessLine>
{
    public void Configure(EntityTypeBuilder<BusinessLine> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new BusinessLine { Id = 1, Name = "Bancolombia S.A" },
            new BusinessLine { Id = 2, Name = "Leasing" },
            new BusinessLine { Id = 3, Name = "Sufi" },
            new BusinessLine { Id = 4, Name = "Renting" }
        );
    }
}