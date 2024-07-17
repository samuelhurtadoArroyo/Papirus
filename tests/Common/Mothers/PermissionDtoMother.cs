namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class PermissionDtoMother
{
    public static PermissionDto Create(int id, string name, string description, string labelCode)
    {
        return new PermissionDtoBuilder()
            .WithId(id)
            .WithName(name)
            .WithDescription(description)
            .WithLabelCode(labelCode)
            .Build();
    }

    public static PermissionDto GetDemandasPermission()
    {
        return Create(1, "Demandas", "Administración de Demandas", "demandas.view");
    }

    public static PermissionDto DefaultPermission()
    {
        return Create(1, "Default", "Default Permision", "default.view");
    }

    public static PermissionDto GetTutelasPermission()
    {
        return Create(2, "Tutelas", "Administración Tutelas", "tutelas.view");
    }

    public static PermissionDto GetConfiguracionPermission()
    {
        return Create(3, "Configuracion", "Configuración", "configuracion.view");
    }

    public static PermissionDto GetEmptyPermission()
    {
        return Create(1, null!, null!, null!);
    }

    public static PermissionDto GetPermissionWithMaxLengths()
    {
        return Create(1, "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A'), "A".PadRight(ValidationConst.MaxFieldLongLength + 1, 'A'), "permission");
    }

    public static List<PermissionDto> GetPermissionList()
    {
        return [
            GetDemandasPermission(),
            GetTutelasPermission(),
            GetConfiguracionPermission()
        ];
    }
}