namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class ProcessTemplateConfiguration : IEntityTypeConfiguration<ProcessTemplate>
{
    public void Configure(EntityTypeBuilder<ProcessTemplate> builder)
    {
        builder.HasOne(d => d.Firm).WithMany(p => p.ProcessTemplates)
            .HasForeignKey(d => d.FirmId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProcessTemplates_Firms");

        builder.HasOne(d => d.Process).WithMany(p => p.ProcessTemplates)
            .HasForeignKey(d => d.ProcessId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProcessTemplates_Processes");

        builder.HasOne(d => d.ProcessType).WithMany(p => p.ProcessTemplates)
            .HasForeignKey(d => d.ProcessTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ProcessTemplates_ProcessTypes");

        builder.HasOne(d => d.SubProcess).WithMany(p => p.ProcessTemplates)
            .HasForeignKey(d => d.SubProcessId)
            .HasConstraintName("FK_ProcessTemplates_SubProcesses");

        builder.HasData(
           new ProcessTemplate { Id = 1, FirmId = 1, ProcessTypeId = 2, ProcessId = 8, SubProcessId = null, FileName = "PLANTILLA 1. CONTESTACION SENCILLA TUTELA.docx", FilePath = "Templates\\Guardianships" },
           new ProcessTemplate { Id = 2, FirmId = 1, ProcessTypeId = 2, ProcessId = 8, SubProcessId = null, FileName = "PLANTILLA 2. ESCRITO DE EMERGENCIA.docx", FilePath = "Templates\\Guardianships" }
       );
    }
}