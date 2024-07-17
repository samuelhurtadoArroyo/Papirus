namespace Papirus.WebApi.Api.Validators;

public class TeamMemberValidator : AbstractValidator<TeamMemberDto>
{
    public TeamMemberValidator()
    {
        RuleFor(m => m.TeamId)
            .NotNull();

        RuleFor(m => m.MemberId)
            .NotNull();

        RuleFor(m => m.IsLead)
            .NotNull();

        RuleFor(m => m.MaxCases)
            .NotNull()
            .GreaterThan(0);
    }
}