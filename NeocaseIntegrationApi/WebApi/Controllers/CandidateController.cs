using AutoMapper;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.Candidates.Commands.CreateCandidateToNeocase;
using UseCases.Candidates.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CandidateController> _logger;
        private readonly IMediator _mediator;

        public CandidateController(IMapper mapper, ILogger<CandidateController> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("create-candidate")]
        public async Task<ActionResult> Create([FromBody] Candidate candidate)
        {
            _logger.LogInformation("!!!!!!!!create-candidate!!!!!!!!!!!!!");
            var cand = _mapper.Map<CandidateDto>(candidate);
            var result = await _mediator.Send(new CreateCandidateToNeocaseCommand { Candidate = cand });
            if (result)
                return Ok();

            return BadRequest("Возникла ошибка при создании заявки");
        }
    }
}