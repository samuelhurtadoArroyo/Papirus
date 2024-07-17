using Microsoft.AspNetCore.Mvc;

namespace Papirus.Services.Email.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public async Task<string> Get()
    {
        return await Task.FromResult("");
    }
}