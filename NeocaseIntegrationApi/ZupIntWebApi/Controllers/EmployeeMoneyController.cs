using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebIntegrations.Connected_Services.OCOWebIntegrations.Soap1c;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using NeocaseProviderLibrary.Providers;

namespace ZupIntWebApi.Controllers
{
    
    [ApiController]
    [Authorize]
    public class EmployeeMoney : ControllerBase
    {
        private readonly ILogger<EmployeeMoney> _logger;
        private readonly InthOCOPortTypeClient _inthOCOPortTypeClient;
        private readonly IPermissionRepositiry _permissionRepositiry;
        private readonly IConfiguration _configuration;
        private readonly NeocaseRootProvider _neocase;

        public EmployeeMoney(ILogger<EmployeeMoney> logger, InthOCOPortTypeClient inthOCOPortTypeClient,
            IPermissionRepositiry permissionRepositiry, 
            IConfiguration configuration,
            NeocaseRootProvider neocase)
        {
            _logger = logger;
            _inthOCOPortTypeClient = inthOCOPortTypeClient;
            _permissionRepositiry = permissionRepositiry;
            _configuration = configuration;
            _neocase = neocase;
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public ContentResult Test()
        {
            //var response = new HttpResponseMessage();
            //response.Content = new StringContent("<div>Hello World</div>");
            //response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"); //new MediaTypeHeaderValue("text/html");
            //response.Content.Headers.Add("Access-Control-Allow-Origin", "*");//Append(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("", "") });
            //return response;

            var result = new ContentResult
            {
                ContentType = "text/html",
                Content = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    
    
</head>
<body>
    
    <div>
    <h3 >This is inside of IFrame!!!</h3>
   
</div> 
</body>
</html>",

            };
            Response.Headers.AccessControlAllowMethods= "POST, GET, OPTIONS";
            Response.Headers.AccessControlAllowOrigin = "*";

            return result;
            // return base.Content($"<h1>Hello!</h1>", "text/html");
        }
        
        private EmployeeMoneyResponse GetTestData(string pernr)
        {
            return new EmployeeMoneyResponse
            {
                @return = new WebIntegrations.Connected_Services.OCOWebIntegrations.Soap1c.EmployeeMoney
                {
                    BONUS = 20000m,
                    DISTRICTCOEFFICIENT = 1.15,
                    FOT = 100000,
                    NOTHERNBONUS = 0,
                    PERNR = pernr,
                    SALARY = 80000
                }
            };
        }
        //https://ocobotest.ibs.ru/intervention/i_qa_frm.asp?numero=20005451&codelangue=2
        [HttpGet("save-employee-money-to-case")]
        [AllowAnonymous]
        public async Task<ActionResult> SaveEmpMoney(string pernr, long numero)
        {
            bool resp = false;
            try
            {
                Response.Headers.AccessControlAllowMethods = "POST, GET, OPTIONS";
                Response.Headers.AccessControlAllowOrigin = "https://ocobotest.ibs.ru";
                Response.Headers.AccessControlAllowCredentials = "true";
                Response.Headers.AccessControlAllowHeaders = "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
                EmployeeMoneyResponse result = null;
                if (pernr == "36")
                {
                    result = GetTestData(pernr);
                }
                else
                {
                    result = await _inthOCOPortTypeClient.EmployeeMoneyAsync(new EmployeeMoneyRequest
                    {
                        CURRENTPERNR = pernr
                    });
                }
                
                resp = await _neocase.NeocaseContactProvider.SetContactCFValueAsync(pernr,109,result.@return.SALARY.ToString());
                resp = await _neocase.NeocaseContactProvider.SetContactCFValueAsync(pernr, 103, result.@return.NOTHERNBONUS.ToString());
                resp = await _neocase.NeocaseContactProvider.SetContactCFValueAsync(pernr, 110, result.@return.BONUS.ToString());
                resp = await _neocase.NeocaseContactProvider.SetContactCFValueAsync(pernr, 111, result.@return.DISTRICTCOEFFICIENT.ToString());
                resp = await _neocase.NeocaseCaseProvider.UpdateChampi(numero, 190, result.@return.FOT.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(GetEmpMoney)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { Success = resp });
        }

        [HttpGet("get-employee-money")]       
        public async Task<ActionResult> GetEmpMoney(string pernr)
        {
            try
            {              
                Response.Headers.AccessControlAllowMethods = "POST, GET, OPTIONS";
                Response.Headers.AccessControlAllowOrigin = "https://ocotest1.ibs.ru";
                Response.Headers.AccessControlAllowCredentials = "true";
                Response.Headers.AccessControlAllowHeaders = "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time";
                IConfigurationSection myArraySection = _configuration.GetSection("PermissionRoles");
                EmployeeMoneyResponse result = null;

                //тестовый костыль
                

                var roles = _configuration.GetSection("PermissionRoles").Get<int[]>();
                if (await _permissionRepositiry.HasPermissionForEmpMoneyAsync(pernr, HttpContext.User.Identity.Name, roles))
                {
                    if (pernr == "36")
                    {
                        result = GetTestData(pernr);
                        return Ok(new { Result = result.@return, User = HttpContext.User.Identity.Name });
                    }
                    try
                    {
                        result = await _inthOCOPortTypeClient.EmployeeMoneyAsync(new EmployeeMoneyRequest
                        {
                            CURRENTPERNR = pernr
                        });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                    return Ok(new { Result = result.@return, User = HttpContext.User.Identity.Name });
                }
                else
                {
                    return Ok(new { Errormess = $"Нет прав доступа для {HttpContext.User.Identity.Name}" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Undhandled error in {nameof(GetEmpMoney)} method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
