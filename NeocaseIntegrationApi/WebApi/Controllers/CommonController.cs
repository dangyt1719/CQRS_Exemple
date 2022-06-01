using MediatR;
using Microsoft.AspNetCore.Mvc;
using NeocaseProviderLibrary.Dto;
using UseCases.NeocaseContacts.Queries.GetAllNeocaseContacts;
using UseCases.Relations.Queries.GetAllRelations;
using UseCases.CaseHistory.Queries.GetCaseHistory;
using UseCases.UpdateChampi.Queries.UpdateChampi;
using UseCases.SetHrRcPartner.Commands.SetHrRcPartner;

namespace WebApi.Controllers
{
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ILogger<CommonController> _logger;
        private readonly IMediator _mediator;

        public CommonController(ILogger<CommonController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("get-all-neocase-contacts")]
        public async Task<ActionResult<ContactDto>> GetAllNeocaseContacts()
        {
            try
            {
                var result = await _mediator.Send(new GetAllNeocaseContactsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-case-history-step-count")]
        public async Task<ActionResult<string>> GetCaseHistory(string caseId, long stepId)
        {
            try
            {
                var result = await _mediator.Send(new GetCaseHistoryQuery() { CaseId = caseId, StepId = stepId });
                return Ok(new { Count = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("update-champi")]
        public async Task<ActionResult<string>> UpdateChampi(string caseId, string champiId)
        {
            try
            {
                var result = await _mediator.Send(new UpdateChampiQuery()
                {
                    Numero = long.Parse(caseId),
                    ChampiId = int.Parse(champiId),
                    Count = 5
                });
                return Ok(new { Count = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all-relations")]
        public async Task<ActionResult<IEnumerable<ContactRelationDto>>> GetAllRelations()
        {
            try
            {
                var result = await _mediator.Send(new GetAllRelationsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("set-hr-rc-partner")]
        public async Task<ActionResult<string>> SetHrRcPartner(long Numero, string MvzGoTo)
        {
            try
            {
                var result = await _mediator.Send(new SetHrRcPartnerCommand()
                {
                    Numero = Numero,
                    MVZ=MvzGoTo
                }
                );
                return Ok(new { Count = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}