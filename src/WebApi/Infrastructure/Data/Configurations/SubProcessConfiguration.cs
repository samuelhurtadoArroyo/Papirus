namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class SubProcessConfiguration : IEntityTypeConfiguration<SubProcess>
{
    public void Configure(EntityTypeBuilder<SubProcess> builder)
    {
        builder.Property(e => e.Description).HasMaxLength(300);
        builder.Property(e => e.Abbreviation).HasMaxLength(50);

        builder.HasOne(d => d.Process).WithMany(p => p.SubProcesses)
            .HasForeignKey(d => d.ProcessId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SubProcesses_Processes");

        builder.HasData(
            new SubProcess { Id = 1, Description = "Pagaré sin abonos, 1 demandado con medida Deceval", Abbreviation = "PSAD-MD", ProcessId = 1 },
            new SubProcess { Id = 2, Description = "Pagaré con abonos, 1 demandado y medidas previas", Abbreviation = "PCAD-MP", ProcessId = 1 },
            new SubProcess { Id = 3, Description = "Pagaré sin abono, 1 pagaré con abono, 1 demandado y con medida Deceval", Abbreviation = "PSAPCAD-MD", ProcessId = 1 },
            new SubProcess { Id = 4, Description = "Dos pagares sin abonos, varios demandados con medida previa", Abbreviation = "2PSAVD-MP", ProcessId = 1 },
            new SubProcess { Id = 5, Description = "Varios pagares, 1 demandado con medida Deceval", Abbreviation = "VPAD-MD", ProcessId = 1 },
            new SubProcess { Id = 6, Description = "Derecho de Petición", ProcessId = 9 },
            new SubProcess { Id = 7, Description = "Salud", ProcessId = 9 },
            new SubProcess { Id = 8, Description = "Habeas Data", ProcessId = 9 }
        );
    }
}