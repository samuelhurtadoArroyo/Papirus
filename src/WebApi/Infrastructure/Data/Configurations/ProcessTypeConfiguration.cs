namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class ProcessTypeConfiguration : IEntityTypeConfiguration<ProcessType>
{
    public void Configure(EntityTypeBuilder<ProcessType> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasData(
            new ProcessType { Id = 1, Name = "Demandas" },
            new ProcessType { Id = 2, Name = "Tutelas" }
        );
    }
}