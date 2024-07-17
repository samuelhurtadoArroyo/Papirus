using Papirus.WebApi.Application.Interfaces;

namespace Papirus.Tests.Common.Mapping;

[ExcludeFromCodeCoverage]
public static class MapperCreator
{
    public static IMapper CreateMapper()
    {
        var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
        return mapperConfig.CreateMapper();
    }
}