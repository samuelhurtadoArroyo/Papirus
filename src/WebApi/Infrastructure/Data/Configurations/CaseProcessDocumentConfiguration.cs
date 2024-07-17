namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class CaseProcessDocumentConfiguration : IEntityTypeConfiguration<CaseProcessDocument>
{
    public void Configure(EntityTypeBuilder<CaseProcessDocument> builder)
    {
        builder.HasOne(d => d.Case).WithMany(p => p.CaseProcessDocuments)
            .HasForeignKey(d => d.CaseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseProcessDocuments_Cases");

        builder.HasOne(d => d.DocumentType).WithMany(p => p.CaseProcessDocuments)
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseProcessDocuments_DocumentTypes");

        builder.HasOne(d => d.ProcessDocumentType).WithMany(p => p.CaseProcessDocuments)
            .HasForeignKey(d => d.ProcessDocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CaseProcessDocuments_ProcessDocumentTypes");
    }
}