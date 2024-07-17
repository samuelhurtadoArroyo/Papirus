using AutoMapper;
using Papirus.WebApi.Application.Mapping;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.Configure<EmailOptions>(configuration.GetSection("EmailOptions"));
        services.Configure<EmailServiceAdOptions>(configuration.GetSection("EmailServiceAdOptions"));

        services.AddSingleton<IEmailAuthenticationService, EmailAuthenticationService>();
        services.AddSingleton<IHtmlToTextConverter, HtmlToTextConverter>();

        services.AddTransient<IAttachmentExtractor, AttachmentExtractor>();

        services.AddHostedService<EmailListenerService>();

        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();

        services.AddScoped<ICaseDocumentFieldValueRepository, CaseDocumentFieldValueRepository>();

        services.AddScoped<ICaseService, CaseService>();
        services.AddScoped<IPersonService, PersonService>();

        services.AddScoped<ICaseProcessDocumentRepository, CaseProcessDocumentRepository>();
        services.AddScoped<IActorRepository, ActorRepository>();

        services.AddScoped<ICaseProcessDocumentService, CaseProcessDocumentService>();
        services.AddScoped<IActorService, ActorService>();

        services.AddScoped<IDeadLineDateService, DeadLineDateService>();
        services.AddScoped<IHolidayRepository, HolidayRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    public static IServiceCollection AddEmailApiVersioningAndSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Swagger generation with API versioning
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataManagerApiOptions>(configuration.GetSection("DataManagerApiOptions"));
        services.Configure<DataExtractorApiOptions>(configuration.GetSection("DataExtractorApiOptions"));

        services.AddSingleton<TokenProvider>();
        services.AddScoped<IDataManagerService, DataManagerService>(provider =>
        {
            var dataManagerOptions = provider.GetRequiredService<IOptions<DataManagerApiOptions>>().Value;
            var tokenProvider = provider.GetRequiredService<TokenProvider>();
            var httpServiceLogger = provider.GetRequiredService<ILogger<HttpService>>();
            var logger = provider.GetRequiredService<ILogger<DataManagerService>>();

            var apiCredentials = new ApiCredentials
            {
                BaseUrl = dataManagerOptions.BaseUrl,
                TokenUrl = dataManagerOptions.TokenUrl,
                ClientId = dataManagerOptions.ClientId,
                ClientSecret = dataManagerOptions.ClientSecret
            };

            var httpService = new HttpService(apiCredentials, tokenProvider, ApiIdentifier.DataManager, httpServiceLogger);
            return new DataManagerService(httpService, logger);
        });

        services.AddScoped<IDataExtractorService, DataExtractorService>(provider =>
        {
            var dataExtractorOptions = provider.GetRequiredService<IOptions<DataExtractorApiOptions>>().Value;
            var tokenProvider = provider.GetRequiredService<TokenProvider>();
            var httpServiceLogger = provider.GetRequiredService<ILogger<HttpService>>();
            var serviceLogger = provider.GetRequiredService<ILogger<DataExtractorService>>();

            var apiCredentials = new ApiCredentials
            {
                BaseUrl = dataExtractorOptions.BaseUrl,
                TokenUrl = dataExtractorOptions.TokenUrl,
                ClientId = dataExtractorOptions.ClientId,
                ClientSecret = dataExtractorOptions.ClientSecret
            };

            var httpService = new HttpService(apiCredentials, tokenProvider, ApiIdentifier.DataExtractor, httpServiceLogger, useAuthentication: false);
            return new DataExtractorService(httpService, serviceLogger);
        });

        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}