namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class ProcessDocumentTypeConfiration : IEntityTypeConfiguration<ProcessDocumentType>
{
    public void Configure(EntityTypeBuilder<ProcessDocumentType> builder)
    {
        builder.HasOne(d => d.DocumentType).WithMany(p => p.ProcessDocumentTypes)
            .HasForeignKey(d => d.DocumentTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProcessDocumentTypes_DocumentTypes");

        builder.HasOne(d => d.Process).WithMany(p => p.ProcessDocumentTypes)
            .HasForeignKey(d => d.ProcessId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProcessDocumentTypes_Processes");

        builder.HasOne(d => d.ProcessTemplate).WithMany(p => p.ProcessDocumentTypes)
            .HasForeignKey(d => d.ProcessTemplateId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProcessDocumentTypes_ProcessTemplates");
    }
}