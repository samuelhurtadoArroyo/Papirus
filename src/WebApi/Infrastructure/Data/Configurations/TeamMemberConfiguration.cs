namespace Papirus.WebApi.Infrastructure.Data.Configurations;

[ExcludeFromCodeCoverage]
public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> builder)
    {
        builder.HasOne(d => d.Member).WithMany(p => p.TeamMembers)
            .HasForeignKey(d => d.MemberId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TeamMembers_Users");

        builder.HasOne(d => d.Team).WithMany(p => p.TeamMembers)
            .HasForeignKey(d => d.TeamId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TeamMembers_Teams");
    }
}