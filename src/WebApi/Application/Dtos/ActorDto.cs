using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Dtos;

/// <summary>
/// Represents an actor within the system.
/// </summary>
public class ActorDto
{
    /// <summary>
    /// The unique identifier for the actor.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The type of the actor (Claimer or Defendant), as defined by the ActorTypeId enum. Defaults to ActorTypeId.Claimer.
    /// </summary>
    public ActorTypeId ActorTypeId { get; set; } = ActorTypeId.Claimer;

    /// <summary>
    /// The identifier for the person associated with this actor. Null if not associated.
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// The  identifier for the case associated with this actor. Null if not associated.
    /// </summary>
    public int? CaseId { get; set; }
}