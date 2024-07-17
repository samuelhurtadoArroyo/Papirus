namespace Papirus.WebApi.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var user = await _appDbContext.Users.Where(u => u.Email.ToLower().Equals(email.ToLower())).FirstOrDefaultAsync();

        return user;
    }
}