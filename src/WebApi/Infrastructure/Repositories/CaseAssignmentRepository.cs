namespace Papirus.WebApi.Infrastructure.Repositories;

public class CaseAssignmentRepository : Repository<Assignment>, ICaseAssignmentRepository
{
    private readonly AppDbContext _appDbContext;

    public CaseAssignmentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Assignment?> GetAssignmentsByCaseIdAsync(int caseId)
    {
        return await _appDbContext.Assignments
            .Where(a => a.CaseId == caseId)
            .Include(a => a.Case)
            .Include(a => a.Status)
            .Include(a => a.TeamMember)
            .FirstOrDefaultAsync();
    }

    public async Task AddAssignmentAsync(Assignment assignment)
    {
        await _appDbContext.Assignments.AddAsync(assignment);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAssignmentAsync(Assignment assignment)
    {
        _appDbContext.Assignments.Update(assignment);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsByTeamMemberIdAsync(int teamMemberId)
    {
        return await _appDbContext.Assignments
            .Where(a => a.TeamMemberId == teamMemberId)
            .Include(a => a.Case)
            .Include(a => a.Status)
            .Include(a => a.TeamMember)
            .ToListAsync();
    }
}