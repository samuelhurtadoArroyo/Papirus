namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
internal class CaseAssignmentControllerTests
{
    private CaseAssignmentController _controller = null!;

    private Mock<ICaseAssignmentService> _mockCaseService = null!;

    private Mock<IMapper> _mockMapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockCaseService = new Mock<ICaseAssignmentService>();
        _mockMapper = new Mock<IMapper>();

        _controller = new CaseAssignmentController(_mockCaseService.Object, _mockMapper.Object);
    }

    [Test]
    public async Task AssignCaseToTeamMember_WhenCalled_ReturnsCreatedWithResult()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 2;
        const int caseStatus = 3;
        var caseAssignment = AssignmentMother.Create(caseId, caseId, teamMemberId, caseStatus);

        var caseAssignmentDto = new CaseAssignmentDto
        {
            Id = caseAssignment.Id,
            CaseId = caseAssignment.CaseId,
            TeamMemberId = caseAssignment.TeamMemberId,
            StatusId = caseAssignment.StatusId
        };

        _mockCaseService.Setup(x => x.AssignCaseToTeamMember(caseId, teamMemberId, caseStatus))
                        .ReturnsAsync(caseAssignmentDto);
        _mockMapper.Setup(x => x.Map<CaseAssignmentDto>(It.IsAny<object>())).Returns(caseAssignmentDto);

        // Act
        var response = await _controller.AssignCaseToTeamMember(caseId, teamMemberId, caseStatus);

        // Assert
        var objectResult = response.Result as CreatedAtActionResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be(StatusCodes.Status201Created);
        objectResult.Value.Should().BeEquivalentTo(caseAssignmentDto);
    }

    [Test]
    public async Task AssignCaseToTeamMember_WhenExceptionIsThrown_ReturnsBadRequest()
    {
        // Arrange
        const int caseId = 1;
        const int teamMemberId = 2;
        const int caseStatus = 3;

        _mockCaseService.Setup(x => x.AssignCaseToTeamMember(caseId, teamMemberId, caseStatus)).Throws<InvalidOperationException>();

        // Act
        var response = await _controller.AssignCaseToTeamMember(caseId, teamMemberId, caseStatus);

        // Assert
        response.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenTeamMemberIdIsValid_ReturnsOk()
    {
        // Arrange
        const int teamMemberId = 1;
        List<TeamMemberAssignmentDto> teamMemberAssignmentDtos = [];
        teamMemberAssignmentDtos.Add(new TeamMemberAssignmentDto
        {
            MemberId = teamMemberId,
            FullName = "",
            CaseLoad = ""
        });

        _mockCaseService.Setup(x => x.GetTeamMembersIfLead(teamMemberId)).ReturnsAsync(teamMemberAssignmentDtos);

        // Act
        var response = await _controller.GetTeamMembersIfLead(teamMemberId);

        // Assert
        response.Result.Should().BeOfType<OkObjectResult>();
        var okResult = response.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult!.Value.Should().BeAssignableTo<IEnumerable<TeamMemberAssignmentDto>>();
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenExceptionIsThrown_ReturnsBadRequest()
    {
        // Arrange
        const int teamMemberId = 1;

        _mockCaseService.Setup(x => x.GetTeamMembersIfLead(teamMemberId)).ThrowsAsync(new InvalidOperationException());

        // Act
        var response = await _controller.GetTeamMembersIfLead(teamMemberId);

        // Assert
        response.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenTeamMemberIdIsNotProvided_ReturnsNotFound()
    {
        // Arrange
        const int teamMemberId = 0;

        // Act
        var response = await _controller.GetTeamMembersIfLead(teamMemberId);

        // Assert
        var notFoundResult = response.Result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenTeamMemberNotFound_ReturnsNotFound()
    {
        // Arrange
        const int teamMemberId = 1;

        _mockCaseService.Setup(x => x.GetTeamMembersIfLead(teamMemberId)).ThrowsAsync(new KeyNotFoundException());

        // Act
        var response = await _controller.GetTeamMembersIfLead(teamMemberId);

        // Assert
        var notFoundResult = response.Result as NotFoundObjectResult;
        notFoundResult.Should().NotBeNull();
        notFoundResult!.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Test]
    public async Task GetTeamMembersIfLead_WhenTeamMemberIsNotLead_ReturnsUnauthorized()
    {
        // Arrange
        const int teamMemberId = 1;

        _mockCaseService.Setup(x => x.GetTeamMembersIfLead(teamMemberId)).ThrowsAsync(new UnauthorizedAccessException());

        // Act
        var response = await _controller.GetTeamMembersIfLead(teamMemberId);

        // Assert
        var unauthorizedResult = response.Result as UnauthorizedObjectResult;
        unauthorizedResult.Should().NotBeNull();
        unauthorizedResult!.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }
}