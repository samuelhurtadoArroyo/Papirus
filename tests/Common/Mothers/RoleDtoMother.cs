using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class RoleDtoMother
{
    public static RoleDto Create(int id, string name)
    {
        return new RoleDtoBuilder()
            .WithId(id)
            .WithName(name)
            .Build();
    }

    public static RoleDto DefaultAdministratorRole()
    {
        return Create(1, "Administrator");
    }

    public static RoleDto DefaultBasicRole()
    {
        return Create(2, "Basic");
    }

    public static RoleDto DefaultSuperRole()
    {
        return Create(3, "Super");
    }

    public static RoleDto DefaultNotFoundRole()
    {
        return Create(100, "NotFoundRoleDto");
    }

    public static RoleDto GetEmptyRole()
    {
        return Create(0, null!);
    }

    public static RoleDto GetRoleWithMaxLengths()
    {
        return Create(1, "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A'));
    }

    public static List<RoleDto> GetRoleList()
    {
        return Enum.GetValues(typeof(RoleType))
            .Cast<RoleType>()
            .Select(r => new RoleDto { Id = (int)r, Name = r.ToString() })
            .ToList();
    }

    public static List<RoleDto> GetRoleList(int quantity)
    {
        var roleFaker = new Faker<RoleDto>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => $"RoleName{f.IndexFaker}");

        return roleFaker.Generate(quantity);
    }

    public static List<RoleDto> GetRandomRoleList(int quantity)
    {
        var roleFaker = new Faker<RoleDto>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => f.Name.Random.AlphaNumeric(50));

        return roleFaker.Generate(quantity);
    }
}