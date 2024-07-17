namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class ActorBuilder
{
    private int _id;

    private int _actorTypeId;

    private int _personId;

    private int _caseId;

    public ActorBuilder()
    {
        _id = 0;
        _actorTypeId = 1;
        _personId = 0;
        _caseId = 0;
    }

    public ActorBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public ActorBuilder WithActoryTypeId(int actorTypeId)
    {
        _actorTypeId = actorTypeId;
        return this;
    }

    public ActorBuilder WithPersonId(int personId)
    {
        _personId = personId;
        return this;
    }

    public ActorBuilder WithCaseId(int caseId)
    {
        _caseId = caseId;
        return this;
    }

    public Actor Build()
    {
        return new Actor
        {
            Id = _id,
            ActorTypeId = _actorTypeId,
            PersonId = _personId,
            CaseId = _caseId
        };
    }
}