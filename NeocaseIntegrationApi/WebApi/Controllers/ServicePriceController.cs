using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.ServicePrice.DTO;
using UseCases.ServicePrice.Queries.GetProcessPrice;

namespace WebApi.Controllers
{
    [ApiController]
    public class ServicePriceController : ControllerBase
    {
        private readonly ILogger<ServicePriceController> _logger;
        private readonly IMediator _mediator;

        public ServicePriceController(ILogger<ServicePriceController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("get-service-price")]
        public async Task<ActionResult<string>> GetServicePrice([FromBody] CaseInfo caseInfo)
        {
            try
            {
                return Ok(new { Price = await _mediator.Send(new GetProcessPriceQuery { CaseInfo = caseInfo }) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}