using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers.Members;

[ApiController]
[Route("[controller]")]
public class MembersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        return Ok();
    }
}