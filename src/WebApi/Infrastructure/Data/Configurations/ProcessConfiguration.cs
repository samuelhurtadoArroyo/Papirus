namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class ProcessConfiguration : IEntityTypeConfiguration<Process>
{
    public void Configure(EntityTypeBuilder<Process> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasOne(d => d.ProcessType).WithMany(p => p.Processes)
            .HasForeignKey(d => d.ProcessTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Processes_ProcessTypes");

        builder.HasData(
            new Process { Id = 1, Name = "Ejecutivo Singular (Personal)", ProcessTypeId = 1 },
            new Process { Id = 2, Name = "Ejecutivo Hipotecario", ProcessTypeId = 1 },
            new Process { Id = 3, Name = "Pago Directo", ProcessTypeId = 1 },
            new Process { Id = 4, Name = "Abreviado de Restitución", ProcessTypeId = 1 },
            new Process { Id = 5, Name = "Reposición de Títulos", ProcessTypeId = 1 },
            new Process { Id = 6, Name = "Ejecutivo Prendario", ProcessTypeId = 1 },
            new Process { Id = 7, Name = "Ejecutivo Mixto", ProcessTypeId = 1 },
            new Process { Id = 8, Name = "Activas", ProcessTypeId = 2 },
            new Process { Id = 9, Name = "Pasivas", ProcessTypeId = 2 }
        );
    }
}