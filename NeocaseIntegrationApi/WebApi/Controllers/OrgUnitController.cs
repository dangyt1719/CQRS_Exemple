using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using WebIntegrations.Connected_Services.OCOWebIntegrations.Soap1c;

namespace WebApi.Controllers
{
    [ApiController]
    public class OrgUnitController : ApiControllerBase
    {
        private readonly IOrgUnitRepository _repo;
        private readonly ILogger<OrgUnitController> _logger;

        public OrgUnitController(IOrgUnitRepository repo, ILogger<OrgUnitController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("get-all-orgunits")]
        public async Task<ActionResult> GetAllOrgUnits()
        {
            try
            {
                var orgUnits = await _repo.GetAllOrgUnitsAsync();
                return Ok(orgUnits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(FindOrgUnits)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("find")]
        public async Task<ActionResult> FindOrgUnits(string namePattern, int maxCount = 50)
        {
            try
            {
                var orgUnits = await _repo.GetOrgList(namePattern);
                return Ok(orgUnits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(FindOrgUnits)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getMvzInfo")]
        public async Task<ActionResult> GetMvzInfo(string code)
        {
            try
            {
                var mvzInfo = await _repo.GetMvzInfo(code);
                return Ok(mvzInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(GetMvzInfo)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("getRcByPernr")]
        public async Task<ActionResult> GetRcByPernr(string pernr)
        {
            try
            {
                var mvzInfo = await _repo.GetRcByPernr(pernr);
                return Ok(mvzInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(GetRcByPernr)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("get-case-fields")]
        public async Task<ActionResult> GetCaseFields(string caseNum)
        {
            try
            {
                var mvzInfo = await _repo.GetCaseFields(caseNum);
                return Ok(mvzInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(GetRcByPernr)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("test-soap")]
        public async Task<ActionResult> TestSoap(string pernr)
        {
            InthOCOPortTypeClient typeClient = new InthOCOPortTypeClient("InthOCO", "InthOCO2021");
            var result = await typeClient.EmployeeMoneyAsync(new EmployeeMoneyRequest
            {
                CURRENTPERNR = pernr
            });           
            return Ok(result.@return);
        }

        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok("API is work!!!");
        }
    }
}