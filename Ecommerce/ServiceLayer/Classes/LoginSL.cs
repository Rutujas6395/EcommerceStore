using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Registration.CommonLayer.Exceptions;
using Registration.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Login;

namespace Ecommerce.ServiceLayer.Classes
{
    public class LoginSL : ILoginSL
    {
        public readonly ILoginRL _loginRL;
        public readonly ILogger<LoginSL> _logger;
        public readonly IConfiguration _configuration;
        public LoginSL(IConfiguration configuration, ILogger<LoginSL> logger, ILoginRL LoginRL)    //Dependency Injection /Constructor
        {
            _configuration = configuration;
            _logger = logger;
            _loginRL = LoginRL;
        }
        public async Task<LoginRes> Login(LoginReq req)
        {
            LoginRes res = new LoginRes();
            try
            {
                _logger.LogInformation("Enter in Login Service layer");
                if (String.IsNullOrEmpty(req.Email) || String.IsNullOrEmpty(req.Password))
                {
                    throw new Exception(CustomExceptions.ExceptionType.Password_Not_Match_Exception.ToString());
                }
                res = await _loginRL.Login(req);
                if (res.IsSuccess == true)
                {
                    string Key = _configuration["Jwt:Key"];
                    string Issuer = _configuration["Jwt:Issuer"];
                    res.Token = TokenProcessing.CreateToken(res.Email, res.Role, Key, Issuer);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($" service layer exception (Login):{ex}");
                res.IsSuccess = false;
            }
           
            return res;
        }



    }
}
