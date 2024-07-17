namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class ActorConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.HasOne(d => d.ActorType).WithMany(p => p.Actors)
                .HasForeignKey(d => d.ActorTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actors_ActorTypes");

        builder.HasOne(d => d.Case).WithMany(p => p.Actors)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actors_Cases");

        builder.HasOne(d => d.Person).WithMany(p => p.Actors)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actors_People");
    }
}