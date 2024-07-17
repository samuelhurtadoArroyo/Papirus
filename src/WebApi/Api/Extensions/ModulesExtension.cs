using Microsoft.Extensions.Options;
using Papirus.WebApi.Application.Common.Models.Options;
using Papirus.WebApi.Infrastructure.Common.Models;
using Papirus.WebApi.Infrastructure.Services;

namespace Papirus.WebApi.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DataManagerApiOptions>(configuration.GetSection("DataManagerApiOptions"));

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

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IActorService, ActorService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IFirmService, FirmService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();
        services.AddScoped<ICaseProcessDocumentService, CaseProcessDocumentService>();
        services.AddScoped<ICaseDocumentFieldValueService, CaseDocumentFieldValueService>();
        services.AddScoped<IDocumentTemplateProcessService, DocumentTemplateProcessService>();
        services.AddScoped<IProcessTemplatesService, ProcessTemplatesService>();
        services.AddScoped<IProcessDocumentTypesService, ProcessDocumentTypesService>();
        services.AddScoped<ICaseService, CaseService>();
        services.AddScoped<IBusinessLineService, IBussinessLineService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ICaseAssignmentService, CaseAssignmentService>();
        services.AddScoped<IDeadLineDateService, DeadLineDateService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IActorRepository, ActorRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IFirmRepository, FirmRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<ICaseProcessDocumentRepository, CaseProcessDocumentRepository>();
        services.AddScoped<ICaseDocumentFieldValueRepository, CaseDocumentFieldValueRepository>();
        services.AddScoped<IProcessTemplatesRepository, ProcessTemplatesRepository>();
        services.AddScoped<IProcessDocumentTypeRepository, ProcessDocumentTypeRepository>();

        services.AddScoped<IBusinessLineRepository, BusinessLineRepository>();
        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ICaseAssignmentRepository, CaseAssignmentRepository>();
        services.AddScoped<IHolidayRepository, HolidayRepository>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<ActorDto>, ActorValidator>();
        services.AddScoped<IValidator<CaseDto>, CaseValidator>();
        services.AddScoped<IValidator<FirmDto>, FirmValidator>();
        services.AddScoped<IValidator<LoginInputDto>, LoginInputDtoValidator>();
        services.AddScoped<IValidator<PermissionDto>, PermissionValidator>();
        services.AddScoped<IValidator<PersonDto>, PersonValidator>();
        services.AddScoped<IValidator<RoleDto>, RoleValidator>();
        services.AddScoped<IValidator<TeamDto>, TeamValidator>();
        services.AddScoped<IValidator<TeamMemberDto>, TeamMemberValidator>();
        services.AddScoped<IValidator<UserInputDto>, UserInputDtoValidator>();
        services.AddScoped<IValidator<CaseDto>, CaseValidator>();
        services.AddScoped<IValidator<UpdateCaseDocumentFieldValueDto>, UpdateCaseDocumentFieldValueValidator>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationInsightsTelemetry(configuration);

        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }
}