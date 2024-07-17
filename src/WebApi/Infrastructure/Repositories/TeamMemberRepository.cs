namespace Papirus.WebApi.Infrastructure.Repositories;

public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
{
    private readonly AppDbContext _appDbContext;

    public TeamMemberRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<TeamMember>> GetByTeamIdAsync(int teamId)
    {
        return await _appDbContext.TeamMembers.Where(tm => tm.TeamId == teamId).Include(tm => tm.Member).ToListAsync();
    }

    public async Task<TeamMember> GetTeamMemberByIdAsync(int id)
    {
        return await _appDbContext.TeamMembers
            .Where(tm => tm.MemberId == id)
            .Include(tm => tm.Member)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundException($"TeamMember with memberID {id} not found");
    }

    public async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync()
    {
        return await GetAllIncludingAsync(tm => tm.Member);
    }

    public async Task AddTeamMemberAsync(TeamMember teamMember)
    {
        await _appDbContext.TeamMembers.AddAsync(teamMember);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateTeamMemberAsync(TeamMember teamMember)
    {
        _appDbContext.TeamMembers.Update(teamMember);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteTeamMemberAsync(int id)
    {
        var teamMember = await GetTeamMemberByIdAsync(id);
        _appDbContext.TeamMembers.Remove(teamMember);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId)
    {
        return await _appDbContext.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .Include(tm => tm.Member)
            .ToListAsync();
    }
}