namespace Papirus.WebApi.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RoleDto, Role>();
        CreateMap<Role, RoleDto>();

        CreateMap<PermissionDto, Permission>();
        CreateMap<Permission, PermissionDto>();

        CreateMap<FirmDto, Firm>();
        CreateMap<Firm, FirmDto>();

        CreateMap<User, UserDto>();
        CreateMap<UserInputDto, User>();

        CreateMap<Actor, ActorDto>();
        CreateMap<ActorDto, Actor>();

        CreateMap<Person, PersonDto>();
        CreateMap<PersonDto, Person>();

        CreateMap<Team, TeamDto>();
        CreateMap<TeamDto, Team>();

        CreateMap<TeamMember, TeamMemberDto>();
        CreateMap<TeamMemberDto, TeamMember>();

        CreateMap<Case, CaseDto>();
        CreateMap<CaseDto, Case>();

        CreateMap<Case, CaseWithAssignmentDto>();

        CreateMap<CaseProcessDocument, CaseProcessDocumentDto>()
            .ReverseMap();

        CreateMap<BusinessLine, BusinessLineDto>();

        CreateMap<CaseDocumentFieldValue, CaseDocumentFieldValueDto>()
            .ForMember(dest => dest.DocumentTypeName, opt => opt.MapFrom(src => src.DocumentType.Name));

        CreateMap<ProcessDocumentType, ProcessDocumentTypeDto>()
            .ReverseMap();

        CreateMap<ProcessTemplate, ProcessDocumentTypeDto>()
            .ReverseMap();

        CreateMap<TeamMember, TeamMemberAssignmentDto>()
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Member.FirstName} {src.Member.LastName}"))
            .ForMember(dest => dest.CaseLoad, opt => opt.MapFrom(src => $"{src.AssignedCases}/{src.MaxCases}"));

        CreateMap<Assignment, CaseAssignmentDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.TeamMember.Member.FirstName} {src.TeamMember.Member.LastName}"))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));

        CreateMap<Case, GuardianshipDto>()
            .ForMember(dest => dest.DefendantName, opt => opt.MapFrom(src => GetActorName(src, 2)))
            .ForMember(dest => dest.ClaimerName, opt => opt.MapFrom(src => GetActorName(src, 1)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Assignment != null && src.Assignment.Status != null ? src.Assignment.Status.Name : string.Empty))
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Assignment != null && src.Assignment.Status != null ? src.Assignment.Status.Id : 0))
            .ForMember(dest => dest.AssignedTeamMemberName, opt => opt.MapFrom(src => src.Assignment != null && src.Assignment.TeamMember != null && src.Assignment.TeamMember.Member != null ? $"{src.Assignment.TeamMember.Member.FirstName} {src.Assignment.TeamMember.Member.LastName}" : string.Empty))
            .ForMember(dest => dest.SubmissionIdentifier, opt => opt.MapFrom(src => src.SubmissionIdentifier ?? string.Empty))
            .ForMember(dest => dest.AssignedTeamMemberId, opt => opt.MapFrom(src => src.Assignment != null ? src.Assignment.TeamMemberId : 0))
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.Assignment != null && src.Assignment.TeamMember != null ? src.Assignment.TeamMember.MemberId : 0))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }

    [ExcludeFromCodeCoverage]
    private string GetActorName(Case src, int actorTypeId)
    {
        if (src.Actors == null || src.Actors.Count == 0)
        {
            return string.Empty;
        }

        var actor = src.Actors.FirstOrDefault(a => a.ActorTypeId == actorTypeId);
        return actor?.Person?.Name ?? string.Empty;
    }
}