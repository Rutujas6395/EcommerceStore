using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Login;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class LoginController : ControllerBase
    {
        public readonly ILoginSL _loginSL;
        public readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, ILoginSL loginSL)
        {
            _logger = logger;
            _loginSL = loginSL;
        }

        //Login
        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Login(LoginReq req)
        {
            _logger.LogInformation("Enter in Login_API");
            LoginRes res = new LoginRes();
            bool LoginSuccess = true;
            try
            {
                res = await this._loginSL.Login(req);
                if (res.IsSuccess == false)
                {
                    _logger.LogError(" Error Occured in Login-API");
                    bool Status = false;
                    return BadRequest(new { Status });
                }
                res = await this._loginSL.Login(req);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Error occured with :=>{ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { LoginSuccess, Message = "Logged In successfully", data = res });
        }
    }
}
