namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("Logs");

        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        builder.Property(l => l.Message)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.MessageTemplate)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.Level)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.TimeStamp)
            .IsRequired(false)
            .HasColumnType("datetime");

        builder.Property(l => l.Exception)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");

        builder.Property(l => l.Properties)
            .IsRequired(false)
            .HasColumnType("nvarchar(max)");
    }
}