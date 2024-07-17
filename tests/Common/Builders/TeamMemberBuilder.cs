namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class TeamMemberBuilder
{
    private int _id;

    private int _teamId;

    private int _memberId;

    private bool _isLead;

    private int _maxCases;

    public TeamMemberBuilder()
    {
        _id = 0;
        _teamId = 0;
        _memberId = 0;
        _isLead = false;
        _maxCases = 0;
    }

    public TeamMemberBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public TeamMemberBuilder WithTeamId(int teamId)
    {
        _teamId = teamId;
        return this;
    }

    public TeamMemberBuilder WithMemberId(int memberId)
    {
        _memberId = memberId;
        return this;
    }

    public TeamMemberBuilder WithIsLead(bool isLead)
    {
        _isLead = isLead;
        return this;
    }

    public TeamMemberBuilder WithMaxCases(int maxCases)
    {
        _maxCases = maxCases;
        return this;
    }

    public TeamMember Build()
    {
        return new TeamMember
        {
            Id = _id,
            TeamId = _teamId,
            MemberId = _memberId,
            IsLead = _isLead,
            MaxCases = _maxCases
        };
    }
}