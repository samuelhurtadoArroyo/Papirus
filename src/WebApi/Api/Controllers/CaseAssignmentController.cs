namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class CaseAssignmentController : ControllerBase
{
    private readonly ICaseAssignmentService _caseAssignmentService;

    private readonly IMapper _mapper;

    public CaseAssignmentController(ICaseAssignmentService caseAssignmentService, IMapper mapper)
    {
        _caseAssignmentService = caseAssignmentService;
        _mapper = mapper;
    }

    /// <summary>
    /// Assigns a case to a team member.
    /// </summary>
    /// <param name="caseId">The ID of the case.</param>
    /// <param name="userId">The ID of the user assigned as team member.</param>
    /// <param name="caseStatus">The status of the case.</param>
    /// <returns>The assigned case details.</returns>
    /// <response code="201">Case assigned successfully.</response>
    /// <response code="400">Invalid input or business rule violation.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="404">Case or team member not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("assign")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CaseAssignmentDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CaseAssignmentDto>> AssignCaseToTeamMember(int caseId, int userId, int caseStatus)
    {
        try
        {
            var result = await _caseAssignmentService.AssignCaseToTeamMember(caseId, userId, caseStatus);
            var dto = _mapper.Map<CaseAssignmentDto>(result);
            return CreatedAtAction(nameof(AssignCaseToTeamMember), new { id = dto.Id }, dto);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorDetails
            {
                ErrorType = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                Errors = [ex.Message]
            });
        }
    }

    /// <summary>
    /// Retrieves the list of team members if the specified team member is a lead.
    /// </summary>
    /// <param name="teamMemberId">The ID of the team member.</param>
    /// <returns>The list of team members.</returns>
    /// <response code="200">List of team members retrieved successfully.</response>
    /// <response code="400">Invalid input or business rule violation.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="404">Team member not found.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("getListTeamMembers")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamMemberAssignmentDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TeamMemberAssignmentDto>> GetTeamMembersIfLead(int teamMemberId)
    {
        try
        {
            if (teamMemberId == 0)
            {
                return NotFound(new ErrorDetails
                {
                    ErrorType = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                    Errors = ["Team member not found"]
                });
            }

            var result = await _caseAssignmentService.GetTeamMembersIfLead(teamMemberId);
            return Ok(_mapper.Map<IEnumerable<TeamMemberAssignmentDto>>(result));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ErrorDetails
            {
                ErrorType = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                Errors = [ex.Message]
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ErrorDetails
            {
                ErrorType = ReasonPhrases.GetReasonPhrase(StatusCodes.Status401Unauthorized),
                Errors = [ex.Message]
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ErrorDetails
            {
                ErrorType = ReasonPhrases.GetReasonPhrase(StatusCodes.Status404NotFound),
                Errors = [ex.Message]
            });
        }
    }
}