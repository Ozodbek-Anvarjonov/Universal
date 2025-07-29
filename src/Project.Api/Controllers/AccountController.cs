using Microsoft.AspNetCore.Mvc;

namespace Project.Api.Controllers;

public class AccountController : BaseController
{
    [HttpGet("profile")]
    public async ValueTask<IActionResult> GetProfile()
    {
        return Ok();
    }
}