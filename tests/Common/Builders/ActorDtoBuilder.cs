using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class ActorDtoBuilder
{
    private int _id;

    private ActorTypeId _actorTypeId;

    private int? _personId;

    private int? _caseId;

    public ActorDtoBuilder()
    {
        _id = 0;
        _actorTypeId = ActorTypeId.Claimer;
        _personId = 0;
        _caseId = 0;
    }

    public ActorDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public ActorDtoBuilder WithActoryTypeId(ActorTypeId actorTypeId)
    {
        _actorTypeId = actorTypeId;
        return this;
    }

    public ActorDtoBuilder WithPersonId(int? personId)
    {
        _personId = personId;
        return this;
    }

    public ActorDtoBuilder WithCaseId(int? caseId)
    {
        _caseId = caseId;
        return this;
    }

    public ActorDto Build()
    {
        return new ActorDto
        {
            Id = _id,
            ActorTypeId = _actorTypeId,
            PersonId = _personId,
            CaseId = _caseId
        };
    }
}