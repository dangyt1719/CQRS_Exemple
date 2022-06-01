using Microsoft.AspNetCore.Mvc;
using NeocaseProviderLibrary.Providers;

namespace WebApi.Controllers
{
    [ApiController]
    public class DateController:ControllerBase
    {

        private readonly ILogger<DateController> _logger;
        private readonly NeocaseRootProvider _neocaseRootProvider;

        public DateController( ILogger<DateController> logger, NeocaseRootProvider neocaseRootProvider)
        {
            _logger=logger;
            _neocaseRootProvider=neocaseRootProvider;
        }
        [HttpGet("get-holidays")]
        public async Task<ActionResult> GetHolidays()
        {
            try
            {
                var result = await _neocaseRootProvider.NeocaseDbProvider.GetHolidaysAsync();
                result=result.Select(s=>s.Date).ToArray();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in jobdays method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
