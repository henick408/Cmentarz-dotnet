using Cmentarz.Mappers.Deceased;
using Cmentarz.Mappers.Grave;

namespace Cmentarz.Configuration;

public static class ServiceExtensions
{
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddScoped<IGraveMapper, GraveMapper>();
        services.AddScoped<IDeceasedMapper, DeceasedMapper>();
    }
}