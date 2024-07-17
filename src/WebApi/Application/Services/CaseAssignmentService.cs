namespace Papirus.WebApi.Application.Services;

public class CaseAssignmentService : ICaseAssignmentService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    private readonly ICaseAssignmentRepository _assignmentRepository;

    private readonly IMapper _mapper;

    public CaseAssignmentService(ITeamMemberRepository teamMemberRepository, ICaseAssignmentRepository assignmentRepository, IMapper mapper)
    {
        _teamMemberRepository = teamMemberRepository;
        _assignmentRepository = assignmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Assignment>> GetAssignmentsByTeamIdAsync(int teamId)
    {
        var teamMembers = await _teamMemberRepository.GetByTeamIdAsync(teamId);
        var assignments = new List<Assignment>();

        foreach (var teamMember in teamMembers)
        {
            var teamMemberAssignments = await _assignmentRepository.GetAssignmentsByTeamMemberIdAsync(teamMember.Id);
            assignments.AddRange(teamMemberAssignments);
        }

        return assignments;
    }

    public async Task<CaseAssignmentDto> AssignCaseToTeamMember(int caseId, int userId, int caseStatusId)
    {
        var existingAssignment = await _assignmentRepository.GetAssignmentsByCaseIdAsync(caseId);

        if (existingAssignment != null)
        {
            await UpdateExistingAssignment(existingAssignment, userId, caseStatusId);
        }
        else
        {
            existingAssignment = await CreateNewAssignment(caseId, userId, caseStatusId);
        }

        var addedAssignment = await _assignmentRepository.GetByIdIncludingAsync(existingAssignment.Id, a => a.Case, a => a.Status, a => a.TeamMember);

        if (addedAssignment != null)
        {
            addedAssignment.Case.IsAssigned = true;
            await _assignmentRepository.UpdateAsync(addedAssignment);
        }

        return _mapper.Map<CaseAssignmentDto>(addedAssignment);
    }

    private async Task UpdateExistingAssignment(Assignment existingAssignment, int userId, int caseStatusId)
    {
        if (existingAssignment.TeamMember.MemberId == userId)
        {
            existingAssignment.StatusId = caseStatusId;
            existingAssignment.TeamMember.MemberId = userId;
            await _assignmentRepository.UpdateAssignmentAsync(existingAssignment);
        }
        else
        {
            await UpdateTeamMembers(existingAssignment.TeamMember.MemberId, userId);

            var member = await _teamMemberRepository.GetTeamMemberByIdAsync(userId);

            existingAssignment.TeamMemberId = member.Id;
            existingAssignment.StatusId = caseStatusId;
            await _assignmentRepository.UpdateAssignmentAsync(existingAssignment);
        }
    }

    private async Task UpdateTeamMembers(int previousMemberId, int newMemberId)
    {
        var previousTeamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(previousMemberId);
        previousTeamMember.AssignedCases--;
        await _teamMemberRepository.UpdateTeamMemberAsync(previousTeamMember);

        var newTeamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(newMemberId);
        newTeamMember.AssignedCases++;
        await _teamMemberRepository.UpdateTeamMemberAsync(newTeamMember);
    }

    private async Task<Assignment> CreateNewAssignment(int caseId, int userId, int caseStatusId)
    {
        var assignment = new Assignment
        {
            TeamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(userId),
            CaseId = caseId,
            StatusId = caseStatusId,
        };

        await _assignmentRepository.AddAssignmentAsync(assignment);

        var teamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(userId) ?? throw new NotFoundException($"Team member with id {userId} not found");
        teamMember.AssignedCases++;
        await _teamMemberRepository.UpdateTeamMemberAsync(teamMember);

        return assignment;
    }

    public async Task<List<TeamMemberAssignmentDto>> GetTeamMembersIfLead(int teamMemberId)
    {
        var teamMember = await _teamMemberRepository.GetTeamMemberByIdAsync(teamMemberId) ?? throw new NotFoundException($"Team member with id {teamMemberId} not found");
        if (!teamMember.IsLead)
        {
            throw new InvalidOperationException("Team member is not a lead.");
        }

        var teamMembers = await _teamMemberRepository.GetTeamMembersByTeamIdAsync(teamMember.TeamId);

        return _mapper.Map<List<TeamMemberAssignmentDto>>(teamMembers);
    }
}