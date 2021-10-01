using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.RegisterUser;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IRegisterSL _serviceLayer;
        public readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IRegisterSL ServiceLayer)
        {
            _logger = logger;
            _serviceLayer = ServiceLayer;
        }

        // Register (user)
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<IActionResult> RegisterUserDetails(RegisterUserReq req)
        {
            _logger.LogInformation("Enter in Registration-API of User");
            RegisterUserRes res = null;
            bool Success = true;
            try
            {
                res = await this._serviceLayer.RegisterUser(req);
                if (res.IsSuccess == false)
                {
                    // _logger.LogError($" Error Occured {res}");   //Can be used in this way as it will dislay the res in console
                    _logger.LogError(" Error Occured in Registartion _api");
                    bool Status = false;
                    return BadRequest(new { Status });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" Error occured with :=>{ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { Success, Message = "Registered successfully", data = res });
        }

        

    }
}
