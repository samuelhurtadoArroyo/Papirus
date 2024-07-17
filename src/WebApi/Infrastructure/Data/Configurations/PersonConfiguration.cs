namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(300);
        builder.Property(e => e.IdentificationNumber).HasMaxLength(50);
        builder.HasIndex(e => e.GuidIdentifier).IsUnique().HasDatabaseName("UQ_People_GuidIdentifier");

        builder.HasOne(d => d.IdentificationType).WithMany(p => p.People)
            .HasForeignKey(d => d.IdentificationTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_People_IdentificationTypes");

        builder.HasOne(d => d.PersonType).WithMany(p => p.People)
            .HasForeignKey(d => d.PersonTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_People_PersonTypes");

        builder.HasData(new Person
        {
            Id = 1,
            GuidIdentifier = new Guid("4f9d0b04-8031-4b89-9a9d-3f8f90c0f8ec"),
            PersonTypeId = 2,
            Name = "Bancolombia S.A.",
            IdentificationTypeId = 4,
            IdentificationNumber = "890903938-8",
            SupportFileName = "Archivo de Soporte Bancolombia.pdf",
            SupportFilePath = "path/bancolombia1"
        });
    }
}
