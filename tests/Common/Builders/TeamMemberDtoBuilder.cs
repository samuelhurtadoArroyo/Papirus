namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class TeamMemberDtoBuilder
{
    private int _id;

    private int _teamId;

    private int _memberId;

    private bool _isLead;

    private int _maxCases;

    public TeamMemberDtoBuilder()
    {
        _id = 0;
        _teamId = 0;
        _memberId = 0;
        _isLead = false;
        _maxCases = 0;
    }

    public TeamMemberDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public TeamMemberDtoBuilder WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    public TeamMemberDtoBuilder WithMemberId(int memberId)
    {
        _memberId = memberId;
        return this;
    }

    public TeamMemberDtoBuilder WithIsLead(bool isLead)
    {
        _isLead = isLead;
        return this;
    }

    public TeamMemberDtoBuilder WithMaxCases(int maxCases)
    {
        _maxCases = maxCases;
        return this;
    }

    public TeamMemberDto Build()
    {
        return new TeamMemberDto
        {
            Id = _id,
            TeamId = _teamId,
            MemberId = _memberId,
            IsLead = _isLead,
            MaxCases = _maxCases
        };
    }
}