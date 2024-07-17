using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class ActorDtoMother
{
    public static ActorDto Create(int id, ActorTypeId actorTypeId, int? personId, int? caseId)
    {
        return new ActorDtoBuilder()
               .WithId(id)
               .WithActoryTypeId(actorTypeId)
               .WithPersonId(personId)
               .WithCaseId(caseId)
               .Build();
    }

    public static ActorDto ClaimantActor()
    {
        return Create(1, ActorTypeId.Claimer, 1, 1);
    }

    public static ActorDto DefenderActor()
    {
        return Create(2, ActorTypeId.Defendant, 2, 1);
    }

    public static ActorDto NoConfigActor()
    {
        return Create(4, ActorTypeId.Claimer, 1, 1);
    }

    public static ActorDto GetEmptyActor()
    {
        return Create(0, 0, null, null);
    }

    public static ActorDto GetActorWithInvalidActorType()
    {
        return Create(1, (ActorTypeId)4, 1, 1);
    }

    public static List<ActorDto> GetActorList()
    {
        return [
            ClaimantActor(),
            DefenderActor()
        ];
    }
}