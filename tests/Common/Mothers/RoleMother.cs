using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class RoleMother
{
    public static Role Create(int id, string name)
    {
        return new RoleBuilder()
               .WithId(id)
               .WithName(name)
               .Build();
    }

    public static Role DefaultAdministratorRole()
    {
        return Create(1, "Administrator");
    }

    public static Role DefaultBasicRole()
    {
        return Create(2, "Basic");
    }

    public static Role DefaultSuperRole()
    {
        return Create(3, "Super");
    }

    public static Role DefaultNotFoundRole()
    {
        return Create(100, "NotFoundRole");
    }

    public static List<Role> GetRoleList()
    {
        return Enum.GetValues(typeof(RoleType))
                       .Cast<RoleType>()
                       .Select(r => new Role { Id = (int)r, Name = r.ToString() })
                       .ToList();
    }

    public static List<Role> GetRoleList(int quantity)
    {
        var roleList = new List<Role>
        {
            Create(0, string.Empty )
        };

        var roleFaker = new Faker<Role>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => $"RoleName{f.IndexFaker}");

        roleList.AddRange(roleFaker.Generate(quantity));

        return roleList;
    }

    public static List<Role> GetRandomRoleList(int quantity)
    {
        var roleFaker = new Faker<Role>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => f.Name.Random.AlphaNumeric(50));

        return roleFaker.Generate(quantity);
    }
}