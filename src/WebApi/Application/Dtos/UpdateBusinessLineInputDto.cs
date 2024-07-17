namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents the update action when user tries to update the business line for a case.
/// </summary>
public class UpdateBusinessLineInputDto
{
    /// <summary>
    /// The unique identifier for the case.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Business line id associated with the case.
    /// </summary>
    public int BusinessLineId { get; set; }
}