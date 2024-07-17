namespace Papirus.WebApi.Api.Controllers;

[Route("api/v1.0/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService userService)
    {
        _authenticationService = userService;
    }

    // POST api/<ActosController>/login
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginInputDto authenticationDto)
    {
        var token = await _authenticationService.Login(authenticationDto.Email, authenticationDto.Password);
        if (string.IsNullOrWhiteSpace(token))
        {
            return Unauthorized();
        }

        return Ok(new LoginDto { Token = token });
    }
}