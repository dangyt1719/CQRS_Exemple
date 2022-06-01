using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeocaseProviderLibrary.Dto;
using UseCases.Conditions.Queries.GetAllConditions;

namespace WebApi.Controllers
{
    [ApiController]
    public class ConditionsController : ControllerBase
    {
        private readonly ILogger<ConditionsController> _logger;
        private readonly IMediator _mediator;

        public ConditionsController(ILogger<ConditionsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("get-all-conditions")]
        public async Task<ActionResult<IEnumerable<ConditionDto>>> GetAllConditions()
        {
            try
            {
                var result = await _mediator.Send(new GetAllConditionsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}