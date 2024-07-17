namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class PermissionMother
{
    public static Permission Create(int id, string name, string description, string labelCode)
    {
        return new PermissionBuilder()
               .WithId(id)
               .WithName(name)
               .WithDescription(description)
               .WithLabelCode(labelCode)
               .Build();
    }

    public static Permission GetDemandasPermission()
    {
        return Create(1, "Demandas", "Administración de Demandas", "demandas.view");
    }

    public static Permission GetTutelasPermission()
    {
        return Create(2, "Tutelas", "Administración Tutelas", "tutelas.view");
    }

    public static Permission GetConfiguracionPermission()
    {
        return Create(3, "Configuracion", "Configuración", "configuracion.view");
    }

    public static List<Permission> GetPermissionList()
    {
        return [
            GetDemandasPermission(),
            GetTutelasPermission(),
            GetConfiguracionPermission()
        ];
    }

    public static List<Permission> GetPermissionList(int quantity)
    {
        var permissionList = new List<Permission>()
        {
            Create(0, "", "","")
        };

        var permissionFaker = new Faker<Permission>()
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.Name, f => $"PermissionName{f.IndexFaker + 1}")
            .RuleFor(o => o.Description, f => $"PermissionDescription{f.IndexFaker + 1}");

        permissionList.AddRange(permissionFaker.Generate(quantity));

        return permissionList;
    }

    public static List<Permission> GetRandomPermissionList(int quantity)
    {
        var permissionFaker = new Faker<Permission>()
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.Name, f => f.Name.Random.AlphaNumeric(50))
            .RuleFor(o => o.Description, f => f.Name.Random.AlphaNumeric(200));

        return permissionFaker.Generate(quantity);
    }
}