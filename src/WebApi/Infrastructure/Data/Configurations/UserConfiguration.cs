namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Email).HasMaxLength(300);
        builder.Property(e => e.FirstName).HasMaxLength(50);
        builder.Property(e => e.LastName).HasMaxLength(50);
        builder.Property(e => e.RegistrationDate).HasColumnType("datetime");

        builder.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

        builder.HasOne(d => d.Firm).WithMany(p => p.Users)
            .HasForeignKey(d => d.FirmId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Users_Firms");

        builder.HasOne(d => d.Role).WithMany(p => p.Users)
            .HasForeignKey(d => d.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Users_Roles");

        builder.HasData(
            new User { Id = 1, Email = "Papirus.Administrator@email.com", FirstName = "Papirus", LastName = "Administrador", HashedPassword = "tXJlkMcMxJhHvZ2RK6SQShIrBzWAJjPwkFHQLz23GTY=", Salt = "YzbbWdkVjn3JNFe1l/IJmA==", RegistrationDate = new DateTime(2024, 02, 28, 04, 18, 10, DateTimeKind.Utc), IsActive = true, RoleId = 1, FirmId = 1 },
            new User { Id = 2, Email = "Basic.User@email.com", FirstName = "Basic", LastName = "User", HashedPassword = "TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=", Salt = "JMHuzQKqE5CXuOTpPkqjDw==", RegistrationDate = new DateTime(2024, 02, 28, 04, 18, 10, DateTimeKind.Utc), IsActive = true, RoleId = 2, FirmId = 1 },
            new User { Id = 3, Email = "Super.User@email.com", FirstName = "Super", LastName = "User", HashedPassword = "pZTwjSj8Iz7tLE/nKcW8v6Fl89YLPOMiLbQ4KmVNlLk=", Salt = "/x2XaXyVuu6cAKTsIQJBgQ==", RegistrationDate = new DateTime(2024, 02, 28, 04, 18, 10, DateTimeKind.Utc), IsActive = true, RoleId = 3, FirmId = 1 }
        );
    }
}