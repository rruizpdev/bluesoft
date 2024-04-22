using Bluesoft.Bank.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bluesoft.Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
       _accountService = accountService;

    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _accountService.GetAsync());
    }
}
