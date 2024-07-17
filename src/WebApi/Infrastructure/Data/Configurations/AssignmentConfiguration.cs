namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.HasOne(d => d.Status).WithMany(p => p.Assignments)
            .HasForeignKey(d => d.StatusId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Assignments_Status");

        builder.HasOne(d => d.TeamMember).WithMany(p => p.Assignments)
            .HasForeignKey(d => d.TeamMemberId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Assignments_TeamMembers");

        builder.HasOne(d => d.Case).WithOne(p => p.Assignment)
            .HasForeignKey<Assignment>(d => d.CaseId)
            .IsRequired()
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Assignments_Cases");
    }
}