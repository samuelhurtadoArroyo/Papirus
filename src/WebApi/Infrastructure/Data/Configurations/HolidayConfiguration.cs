namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.Property(e => e.Description).HasMaxLength(128);
        builder.Property(e => e.Date).HasColumnType("datetime");

        builder.HasData(
            new Holiday { Id = 1, Date = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), Description = "New year's day" },
            new Holiday { Id = 2, Date = new(2024, 1, 8, 0, 0, 0, DateTimeKind.Utc), Description = "Epiphany" },
            new Holiday { Id = 3, Date = new(2024, 3, 25, 0, 0, 0, DateTimeKind.Utc), Description = "Saint Joseph's day" },
            new Holiday { Id = 4, Date = new(2024, 3, 28, 0, 0, 0, DateTimeKind.Utc), Description = "Maundy Thursday" },
            new Holiday { Id = 5, Date = new(2024, 3, 29, 0, 0, 0, DateTimeKind.Utc), Description = "Great Friday" },
            new Holiday { Id = 6, Date = new(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc), Description = "Labor day" },
            new Holiday { Id = 7, Date = new(2024, 5, 13, 0, 0, 0, DateTimeKind.Utc), Description = "Ascension day" },
            new Holiday { Id = 8, Date = new(2024, 6, 3, 0, 0, 0, DateTimeKind.Utc), Description = "Corpus Christi" },
            new Holiday { Id = 9, Date = new(2024, 6, 10, 0, 0, 0, DateTimeKind.Utc), Description = "Sacred Heart" },
            new Holiday { Id = 10, Date = new(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc), Description = "St Peter and Saint Paul Day" },
            new Holiday { Id = 11, Date = new(2024, 7, 20, 0, 0, 0, DateTimeKind.Utc), Description = "Independence Day" },
            new Holiday { Id = 12, Date = new(2024, 8, 7, 0, 0, 0, DateTimeKind.Utc), Description = "Battle of Boyacá Day" },
            new Holiday { Id = 13, Date = new(2024, 8, 19, 0, 0, 0, DateTimeKind.Utc), Description = "Assumption of mary" },
            new Holiday { Id = 14, Date = new(2024, 10, 14, 0, 0, 0, DateTimeKind.Utc), Description = "Columbus Day" },
            new Holiday { Id = 15, Date = new(2024, 11, 4, 0, 0, 0, DateTimeKind.Utc), Description = "All Saints Day" },
            new Holiday { Id = 16, Date = new(2024, 11, 11, 0, 0, 0, DateTimeKind.Utc), Description = "Independence of Cartagena" },
            new Holiday { Id = 17, Date = new(2024, 12, 8, 0, 0, 0, DateTimeKind.Utc), Description = "Immaculate Conception" },
            new Holiday { Id = 18, Date = new(2024, 12, 25, 0, 0, 0, DateTimeKind.Utc), Description = "Christmas Day" }
        );
    }
}