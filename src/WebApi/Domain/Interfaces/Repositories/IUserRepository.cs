namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
}