using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.HasIndex(e => e.GuidIdentifier, "UQ_Cases_GuidIdentifier").IsUnique();

        builder.Property(e => e.RegistrationDate).HasColumnType("datetime");
        builder.Property(e => e.Court).HasMaxLength(128);
        builder.Property(e => e.City).HasMaxLength(128);
        builder.Property(e => e.Amount).HasColumnType("money");
        builder.Property(e => e.SubmissionDate).HasColumnType("datetime");
        builder.Property(e => e.SubmissionIdentifier).HasMaxLength(64);
        builder.Property(e => e.DeadLineDate).HasColumnType("datetime");
        builder.Property(o => o.EmailHtmlBody).HasColumnType("nvarchar(MAX)");
        builder.Property(e => e.FilePath).HasMaxLength(1024);
        builder.Property(e => e.FileName).HasMaxLength(512);
        builder.Property(e => e.IsAnswered).HasColumnType("bit");
        builder.Property(e => e.AnsweredDate).HasColumnType("datetime");

        builder.HasOne(d => d.Process).WithMany(p => p.Cases)
            .HasForeignKey(d => d.ProcessId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Cases_Processes");

        builder.HasOne(d => d.ProcessType).WithMany(p => p.Cases)
            .HasForeignKey(d => d.ProcessTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Cases_ProcessTypes");

        builder.HasOne(d => d.SubProcess).WithMany(p => p.Cases)
            .HasForeignKey(d => d.SubProcessId)
            .HasConstraintName("FK_Cases_SubProcesses");
    }
}