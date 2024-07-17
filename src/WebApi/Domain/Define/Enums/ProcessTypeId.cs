namespace Papirus.WebApi.Domain.Define.Enums;

/// <summary>
/// Defines the types of legal processes that can be initiated.
/// </summary>
public enum ProcessTypeId
{
    /// <summary>
    /// Represents a legal demand, a formal action or lawsuit to secure a right against another party.
    /// </summary>
    Demand = 1,

    /// <summary>
    /// Represents a guardianship process (Tutela in Colombia), a legal mechanism designed to protect an individual's personal and property rights when they are unable to do so themselves.
    /// </summary>
    Guardianship = 2
}