using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface IPersonRepository : IRepository<Person>
{
    Task<Person?> FindByIdentificationAsync(IdentificationTypeId identificationTypeId, string? identificationNumber);
}