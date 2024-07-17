namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class CaseDocumentFieldValueConfiguration : IEntityTypeConfiguration<CaseDocumentFieldValue>
{
    public void Configure(EntityTypeBuilder<CaseDocumentFieldValue> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.Tag).HasMaxLength(50);

        builder.HasOne(d => d.CaseProcessDocument).WithMany(p => p.CaseDocumentFieldValues)
            .HasForeignKey(d => d.CaseProcessDocumentId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseDocumentFieldValues_CaseProcessDocuments");

        builder.HasOne(d => d.DocumentType).WithMany(p => p.CaseDocumentFieldValues)
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseDocumentFieldValues_DocumentTypes");

        builder.HasOne(d => d.ProcessDocumentType).WithMany(p => p.CaseDocumentFieldValues)
            .HasForeignKey(d => d.ProcessDocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseDocumentFieldValues_ProcessDocumentTypes");
    }
}