namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class CaseAssignmentRepositoryTests
{
    private List<Assignment> _assignmentList = null!;

    private CaseAssignmentRepository _repository = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    [SetUp]
    public void SetUp()
    {
        _assignmentList = AssignmentMother.GetAssignmentList();
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockAppDbContext.Setup(x => x.Assignments).ReturnsDbSet(_assignmentList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _repository = new CaseAssignmentRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task GetAssignmentsByCaseIdAsync_WhenCaseIdIsValid_ReturnsAssignments()
    {
        // Arrange
        var caseId = _assignmentList[0].CaseId;

        // Act
        var result = await _repository.GetAssignmentsByCaseIdAsync(caseId);

        // Assert
        result.Should().NotBeNull();
        result?.CaseId.Should().Be(caseId);
    }

    [Test]
    public async Task AddAssignmentAsync_WhenCalled_AddsAssignment()
    {
        // Arrange
        var assignment = AssignmentMother.DefaultAssignment();
        var mockDbSet = new Mock<DbSet<Assignment>>();
        _mockAppDbContext.Setup(m => m.Assignments).Returns(mockDbSet.Object);

        // Act
        await _repository.AddAssignmentAsync(assignment);

        // Assert
        mockDbSet.Verify(m => m.AddAsync(assignment, default), Times.Once());
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task UpdateAssignmentAsync_WhenCalled_UpdatesAssignment()
    {
        // Arrange
        var assignment = AssignmentMother.DefaultAssignment();
        var mockDbSet = new Mock<DbSet<Assignment>>();
        _mockAppDbContext.Setup(m => m.Assignments).Returns(mockDbSet.Object);

        // Act
        await _repository.UpdateAssignmentAsync(assignment);

        // Assert
        mockDbSet.Verify(m => m.Update(assignment), Times.Once());
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task GetAssignmentsByTeamMemberIdAsync_WhenCalled_ReturnsAssignments()
    {
        // Arrange
        var teamMemberId = _assignmentList[0].TeamMemberId;
        var expectedAssignments = _assignmentList.Where(a => a.TeamMemberId == teamMemberId).ToList();

        // Act
        var result = await _repository.GetAssignmentsByTeamMemberIdAsync(teamMemberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedAssignments, options => options.Excluding(x => x.Case).Excluding(x => x.Status).Excluding(x => x.TeamMember));
    }
}