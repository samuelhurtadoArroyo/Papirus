using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class User : EntityBase
{
    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public bool IsActive { get; set; } = true;

    public bool MustChangePassword { get; set; }

    public int RoleId { get; set; } = (int)RoleType.Basic;

    public virtual Role Role { get; set; } = null!;

    public int FirmId { get; set; }

    public virtual Firm Firm { get; set; } = null!;

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = [];
}