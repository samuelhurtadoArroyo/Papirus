using AutoMapper;
using Papirus.WebApi.Application.Dtos;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
public class CaseAssignmentServiceTests
{
    private Mock<ITeamMemberRepository> _mockTeamMemberRepository = null!;

    private Mock<ICaseAssignmentRepository> _mockAssignmentRepository = null!;

    private Mock<IMapper> _mockMapper = null!;

    private CaseAssignmentService _service = null!;

    [SetUp]
    public void Setup()
    {
        _mockTeamMemberRepository = new Mock<ITeamMemberRepository>();
        _mockAssignmentRepository = new Mock<ICaseAssignmentRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new CaseAssignmentService(_mockTeamMemberRepository.Object, _mockAssignmentRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetAssignmentsByTeamIdAsync_WhenCalled_ReturnsAssignments()
    {
        // Arrange
        const int teamId = 1;
        var teamMembers = TeamMemberMother.GetTeamMemberList(2);

        teamMembers[0].Id = 1;
        teamMembers[1].Id = 2;

        var teamMemberIds = teamMembers.ConvertAll(tm => tm.Id);
        var assignments = AssignmentMother.GetAssignmentsForTeamMembers(teamMemberIds);

        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(teamId)).ReturnsAsync(teamMembers);
        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByTeamMemberIdAsync(1)).ReturnsAsync(assignments.Where(x => x.TeamMemberId == 1).ToList());
        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByTeamMemberIdAsync(2)).ReturnsAsync(assignments.Where(x => x.TeamMemberId == 2).ToList());

        // Act
        var result = await _service.GetAssignmentsByTeamIdAsync(teamId);

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(2);
    }

    [Test]
    public async Task AssignCaseToTeamMember_WhenAssignmentExistsAndTeamMemberIsSame_UpdatesAssignment()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var existingTeamMember = TeamMemberMother.CreateWithDetails(
            id: teamMemberId,
            assignedCases: 1,
            firstName: "John",
            lastName: "Doe"
        );

        var existingAssignment = AssignmentMother.CreateWithDetails(
            id: 1,
            caseId: caseId,
            teamMemberId: teamMemberId,
            statusId: caseStatusId,
            teamMember: existingTeamMember,
            statusName: "Open"
        );

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(existingAssignment);
        _mockAssignmentRepository.Setup(x => x.GetByIdIncludingAsync(existingAssignment.Id)).ReturnsAsync(existingAssignment);

        // Act
        await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        _mockAssignmentRepository.Verify(x => x.UpdateAssignmentAsync(existingAssignment), Times.Once);
    }

    [Test]
    public async Task AssignCaseToTeamMember_WhenAssignmentExistsAndTeamMemberIsDifferent_UpdatesAssignmentAndTeamMembers()
    {
        // Arrange
        const int caseId = 1;
        const int userId = 2;
        const int caseStatusId = 1;

        var existingTeamMember = TeamMemberMother.CreateWithDetails(
            id: 1,
            assignedCases: 1,
            firstName: "John",
            lastName: "Doe"
        );

        var newTeamMember = TeamMemberMother.CreateWithDetails(
            id: 2,
            assignedCases: 0,
            firstName: "Jane",
            lastName: "Smith"
        );

        var existingAssignment = AssignmentMother.CreateWithDetails(
            id: 1,
            caseId: caseId,
            teamMemberId: 1,
            statusId: 1,
            teamMember: existingTeamMember,
            statusName: "Open"
        );

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(existingAssignment);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(1)).ReturnsAsync(existingTeamMember);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(2)).ReturnsAsync(newTeamMember);
        _mockAssignmentRepository.Setup(x => x.GetByIdIncludingAsync(existingAssignment.Id, a => a.Case, a => a.Status, a => a.TeamMember)).ReturnsAsync(existingAssignment);

        // Act
        await _service.AssignCaseToTeamMember(caseId, userId, caseStatusId);

        // Assert
        _mockAssignmentRepository.Verify(x => x.UpdateAssignmentAsync(existingAssignment), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.UpdateTeamMemberAsync(existingTeamMember), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.UpdateTeamMemberAsync(newTeamMember), Times.Once);
    }

    [Test]
    public async Task AssignCaseToTeamMember_WhenAssignmentDoesNotExist_CreatesAssignmentAndUpdatesTeamMember()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var teamMember = TeamMemberMother.CreateWithDetails(
            id: teamMemberId,
            assignedCases: 0,
            firstName: "John",
            lastName: "Doe"
        );

        var assignment = AssignmentMother.CreateWithDetails(
            id: 1,
            caseId: caseId,
            teamMemberId: teamMemberId,
            statusId: caseStatusId,
            teamMember: teamMember,
            statusName: "Open"
        );

        var expectedDto = new CaseAssignmentDto
        {
            Id = assignment.Id,
            CaseId = caseId,
            TeamMemberId = teamMemberId,
            FullName = "John Doe",
            StatusId = caseStatusId,
            StatusName = "Open"
        };

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync((Assignment?)null);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ReturnsAsync(teamMember);
        _mockAssignmentRepository.Setup(x => x.AddAssignmentAsync(It.IsAny<Assignment>())).Callback<Assignment>(a => a.Id = assignment.Id).Returns(Task.CompletedTask);
        _mockAssignmentRepository.Setup(x => x.GetByIdIncludingAsync(assignment.Id, a => a.Case, a => a.Status, a => a.TeamMember)).ReturnsAsync(assignment);
        _mockMapper.Setup(m => m.Map<CaseAssignmentDto>(assignment)).Returns(expectedDto);

        // Act
        var result = await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        _mockAssignmentRepository.Verify(x => x.AddAssignmentAsync(It.IsAny<Assignment>()), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.UpdateTeamMemberAsync(It.Is<TeamMember>(tm => tm.Id == teamMemberId && tm.AssignedCases == 1)), Times.Once);

        result.Should().BeEquivalentTo(expectedDto);
    }

    [Test]
    public async Task AssignCaseToTeamMember_WhenCaseStatusNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;
        var exceptionMessage = $"Case with id {caseId} not found";

        _mockAssignmentRepository
            .Setup(x => x.GetAssignmentsByCaseIdAsync(caseId))
            .ThrowsAsync(new KeyNotFoundException(exceptionMessage));

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage(exceptionMessage);
    }

    [Test]
    public void AssignCaseToTeamMember_WhenCaseStatusIsNotOpen_ThrowsInvalidOperationException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 2;

        var existingAssignment = AssignmentMother.DefaultAssignment();

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(existingAssignment);

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Case status is not open.");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenCaseNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        _mockAssignmentRepository
            .Setup(x => x.GetAssignmentsByCaseIdAsync(caseId))
            .ReturnsAsync((Assignment?)null);

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Case with id {caseId} not found");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenTeamMemberIsNotLead_ThrowsInvalidOperationException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var teamMember = new TeamMember { Id = teamMemberId, IsLead = false };

        var assignment = AssignmentMother.DefaultAssignment();

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(assignment);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ReturnsAsync(teamMember);

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Team member is not a lead.");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenTeamMemberNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        _mockTeamMemberRepository
            .Setup(x => x.GetTeamMemberByIdAsync(teamMemberId))
            .ThrowsAsync(new KeyNotFoundException($"Team member with id {teamMemberId} not found"));

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Team member with id {teamMemberId} not found");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenAssignmentExistsAndTeamMemberIsSame_ThrowsInvalidOperationException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var existingAssignment = AssignmentMother.DefaultAssignment();

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(existingAssignment);

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Assignment already exists for the team member.");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenAssignmentExistsAndTeamMemberIsDifferent_ThrowsInvalidOperationException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 2;
        const int caseStatusId = 1;

        var existingAssignment = AssignmentMother.DefaultAssignment();

        var previousTeamMember = TeamMemberMother.DefaultTeamMember1();

        var newTeamMember = TeamMemberMother.DefaultTeamMember2();

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(existingAssignment);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(1)).ReturnsAsync(previousTeamMember);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(2)).ReturnsAsync(newTeamMember);

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Assignment already exists for the team member.");
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenTeamMemberIsLead_ReturnsTeamMembers()
    {
        // Arrange
        const int teamMemberId = 1;
        var leadTeamMember = TeamMemberMother.CreateTeamMember(teamMemberId, 1, 1, true, 5);

        var teamMembers = new List<TeamMember>
    {
        TeamMemberMother.CreateTeamMember(1, 1, 1, false, 5),
        TeamMemberMother.CreateTeamMember(2, 1, 2, false, 5)
    };

        var teamMemberDtos = new List<TeamMemberAssignmentDto>
    {
        new() { MemberId = 1, FullName = "Member 1", CaseLoad = "1/5" },
        new() { MemberId = 2, FullName = "Member 2", CaseLoad = "2/3" }
    };

        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ReturnsAsync(leadTeamMember);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMembersByTeamIdAsync(leadTeamMember.TeamId)).ReturnsAsync(teamMembers);
        _mockMapper.Setup(x => x.Map<List<TeamMemberAssignmentDto>>(teamMembers)).Returns(teamMemberDtos);

        // Act
        var result = await _service.GetTeamMembersIfLead(teamMemberId);

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(2);
        result.Should().BeEquivalentTo(teamMemberDtos);
    }

    [Test]
    public void GetTeamMembersIfLead_WhenTeamMemberNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        const int teamMemberId = 1;

        _mockTeamMemberRepository
            .Setup(x => x.GetTeamMemberByIdAsync(teamMemberId))
            .ThrowsAsync(new KeyNotFoundException($"Team member with id {teamMemberId} not found"));

        // Act
        Func<Task> act = async () => await _service.GetTeamMembersIfLead(teamMemberId);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Team member with id {teamMemberId} not found");
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenTeamMemberIsNotLead_ThrowsInvalidOperationException()
    {
        // Arrange
        const int teamMemberId = 1;
        var nonLeadTeamMember = TeamMemberMother.CreateTeamMember(teamMemberId, teamId: 1, memberId: 1, isLead: false, maxCases: 5);

        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ReturnsAsync(nonLeadTeamMember);

        // Act
        Func<Task> act = async () => await _service.GetTeamMembersIfLead(teamMemberId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Team member is not a lead.");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenAssignmentDoesNotExistAndTeamMemberNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync((Assignment?)null);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ThrowsAsync(new KeyNotFoundException($"Team member with id {teamMemberId} not found"));

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Team member with id {teamMemberId} not found");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenAddAssignmentFails_ThrowsException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var teamMember = TeamMemberMother.DefaultTeamMember1();

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync((Assignment?)null);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ReturnsAsync(teamMember);
        _mockAssignmentRepository.Setup(x => x.AddAssignmentAsync(It.IsAny<Assignment>())).ThrowsAsync(new Exception("Add assignment failed"));

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Add assignment failed");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenUpdateAssignmentFails_ThrowsException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var existingTeamMember = TeamMemberMother.CreateWithDetails(
            id: 1,
            assignedCases: 1,
            firstName: "John",
            lastName: "Doe"
        );

        var assignment = AssignmentMother.CreateWithDetails(
            id: 1,
            caseId: caseId,
            teamMemberId: teamMemberId,
            statusId: caseStatusId,
            teamMember: existingTeamMember,
            statusName: "Open"
            );

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync((Assignment?)null);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(teamMemberId)).ReturnsAsync(existingTeamMember);
        _mockAssignmentRepository.Setup(x => x.AddAssignmentAsync(It.IsAny<Assignment>())).Returns(Task.CompletedTask);
        _mockAssignmentRepository.Setup(x => x.AddAssignmentAsync(It.IsAny<Assignment>())).Returns(Task.CompletedTask);

        _mockAssignmentRepository.Setup(x => x.GetByIdIncludingAsync(1, a => a.Case, a => a.Status, a => a.TeamMember)).ReturnsAsync(assignment);

        _mockAssignmentRepository.Setup(x => x.UpdateAssignmentAsync(It.IsAny<Assignment>())).ThrowsAsync(new Exception("Update assignment failed"));

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Update assignment failed");
    }

    [Test]
    public void AssignCaseToTeamMember_WhenUpdateTeamMemberFails_ThrowsException()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 1;
        const int caseStatusId = 1;

        var existingTeamMember = TeamMemberMother.CreateWithDetails(
            id: 1,
            assignedCases: 1,
            firstName: "John",
            lastName: "Doe"
        );

        var assignment = AssignmentMother.CreateWithDetails(
            id: 1,
            caseId: caseId,
            teamMemberId: teamMemberId,
            statusId: caseStatusId,
            teamMember: existingTeamMember,
            statusName: "Open"
            );

        var previousTeamMember = TeamMemberMother.DefaultTeamMember2();

        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByCaseIdAsync(caseId)).ReturnsAsync(assignment);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(1)).ReturnsAsync(previousTeamMember);
        _mockTeamMemberRepository.Setup(x => x.GetTeamMemberByIdAsync(2)).ReturnsAsync(existingTeamMember);
        _mockAssignmentRepository.Setup(x => x.GetByIdIncludingAsync(It.IsAny<int>(), a => a.Case, a => a.Status, a => a.TeamMember)).ReturnsAsync(assignment);
        _mockTeamMemberRepository.Setup(x => x.UpdateTeamMemberAsync(It.IsAny<TeamMember>())).ThrowsAsync(new Exception("Update team member failed"));

        // Act
        Func<Task> act = async () => await _service.AssignCaseToTeamMember(caseId, teamMemberId, caseStatusId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Update team member failed");
    }

    [Test]
    public void GetAssignmentsByTeamIdAsync_WhenTeamMemberNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        const int teamId = 1;

        _mockTeamMemberRepository
            .Setup(x => x.GetByTeamIdAsync(teamId))
            .ThrowsAsync(new KeyNotFoundException($"Team with id {teamId} not found"));

        // Act
        Func<Task> act = async () => await _service.GetAssignmentsByTeamIdAsync(teamId);

        // Assert
        act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Team with id {teamId} not found");
    }

    [Test]
    public void GetAssignmentsByTeamIdAsync_WhenGetAssignmentsByTeamMemberIdFails_ThrowsException()
    {
        // Arrange
        const int teamId = 1;
        var teamMembers = TeamMemberMother.GetTeamMemberList(2);

        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(teamId)).ReturnsAsync(teamMembers);
        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByTeamMemberIdAsync(1)).ThrowsAsync(new Exception("Get assignments by team member id failed"));

        // Act
        Func<Task> act = async () => await _service.GetAssignmentsByTeamIdAsync(teamId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Get assignments by team member id failed");
    }

    [Test]
    public void GetAssignmentsByTeamIdAsync_WhenGetAssignmentsByTeamMemberIdFailsForSecondTeamMember_ThrowsException()
    {
        // Arrange
        const int teamId = 1;
        var teamMembers = TeamMemberMother.GetTeamMemberList(2);

        var assignmentsForFirstMember = AssignmentMother.GetAssignmentList(1)
            .Where(x => x.TeamMemberId == 1)
            .ToList();

        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(teamId)).ReturnsAsync(teamMembers);
        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByTeamMemberIdAsync(1)).ReturnsAsync(assignmentsForFirstMember);
        _mockAssignmentRepository.Setup(x => x.GetAssignmentsByTeamMemberIdAsync(2)).ThrowsAsync(new Exception("Get assignments by team member id failed"));

        // Act
        Func<Task> act = async () => await _service.GetAssignmentsByTeamIdAsync(teamId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Get assignments by team member id failed");
    }

    [Test]
    public void GetAssignmentsByTeamIdAsync_WhenGetByTeamIdFails_ThrowsException()
    {
        // Arrange
        const int teamId = 1;

        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(teamId)).ThrowsAsync(new Exception("Get by team id failed"));

        // Act
        Func<Task> act = async () => await _service.GetAssignmentsByTeamIdAsync(teamId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Get by team id failed");
    }

    [Test]
    public void GetAssignmentsByTeamIdAsync_WhenGetByTeamIdFailsForSecondTeamMember_ThrowsException()
    {
        // Arrange
        const int teamId = 1;
        var teamMembers = TeamMemberMother.GetTeamMemberList(2);

        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(teamId)).ReturnsAsync(teamMembers);
        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(2)).ThrowsAsync(new Exception("Get by team id failed"));

        // Act
        Func<Task> act = async () => await _service.GetAssignmentsByTeamIdAsync(teamId);

        // Assert
        act.Should().ThrowAsync<Exception>().WithMessage("Get by team id failed");
    }
}