using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    private readonly AppDbContext _appDbContext;

    public PersonRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    public async Task<Person?> FindByIdentificationAsync(IdentificationTypeId identificationTypeId, string? identificationNumber)
    {
        if (identificationNumber is null) return null;

        var person = await _appDbContext.People.Where(p => p.IdentificationTypeId == (int)identificationTypeId && p.IdentificationNumber.ToLower().Equals(identificationNumber.ToLower())).FirstOrDefaultAsync();

        return person!;
    }
}