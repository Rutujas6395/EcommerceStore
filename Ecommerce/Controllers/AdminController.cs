using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.AdminRegByAdmin;
using static Ecommerce.CommonLayer.Model.GetAdmins;
using static Ecommerce.CommonLayer.Model.GetUsers;
using static Ecommerce.CommonLayer.Model.Login;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IRegisterSL _serviceLayer;
        public readonly ILogger<AdminController> _logger;
        public readonly IGetAdminsAndUsersSL _getAdminsAndUsersSL;
        
        public AdminController(ILogger<AdminController> logger, IRegisterSL ServiceLayer,IGetAdminsAndUsersSL GetAdminsAndUsers)
        {
            _logger = logger;
            _serviceLayer = ServiceLayer;
            _getAdminsAndUsersSL = GetAdminsAndUsers;
        }

        // Register (Admin By Another Admin)
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminDetails(RegisterAdminReq req)
        {
            _logger.LogInformation("Enter in Registration-API of User");
            RegisterAdminRes res = null;
            bool Success = true;
            try
            {
                res = await this._serviceLayer.AdminRegByAdmin(req);
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


        


        //GetAdmins
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAdmins")]
        public async Task<IActionResult> GetAdmins()
        {
            var resultList = new List<GetAdminsRes>();
            try
            {
                _logger.LogInformation("Enter into GetAdmins API");
                resultList = await this._getAdminsAndUsersSL.GetAdmins();
            }
            catch (Exception ex)
            {
                _logger.LogError($" Get Admins Exception message:{ex}");
            }
            return Ok(resultList);
        }

        //GetUsers
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var resultList = new List<GetUsersRes>();

            try
            {
                _logger.LogInformation("Enter into GetUsers API");
                resultList = await this._getAdminsAndUsersSL.GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError($" GetUsers Exception message:{ex}");
            }

            return Ok(resultList);

        }
    }
}
