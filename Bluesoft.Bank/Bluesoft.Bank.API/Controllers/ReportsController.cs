using Bluesoft.Bank.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bluesoft.Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IAccountMovementService _accountMovementService;

        public ReportsController(IAccountMovementService accountMovementService)
        {
            _accountMovementService = accountMovementService;
        }

        [HttpGet("ClientMonthTransactionsCount")]
        public async Task<IActionResult> GetAsync(
            [FromQuery] int month,
            [FromQuery] int year)
        {
            return Ok(await _accountMovementService.GetClientMonthTransactionsCountAsync(month, year));
        }
    }
}
