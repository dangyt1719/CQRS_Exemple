using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.PowerAttorneyRegistry.Queries.GetPowerAttorneyExcelReport;

namespace WebApi.Controllers
{
    [ApiController]
    public class PowerAttorneyRegistryController : ControllerBase
    {
        private readonly ILogger<PowerAttorneyRegistryController> _logger;
        private readonly IMediator _mediator;

        public PowerAttorneyRegistryController(ILogger<PowerAttorneyRegistryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("get-power-attorney-report")]
        public async Task<IActionResult> GetPowerAttorneyExcelReport()
        {
            try
            {
                var file = await _mediator.Send(new GetPowerAttorneyExcelReportQuery());
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}