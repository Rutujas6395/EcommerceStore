using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Registration.CommonLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.AdminRegByAdmin;
using static Ecommerce.CommonLayer.Model.RegisterUser;


namespace Ecommerce.ServiceLayer.Classes
{
    public class RegisterSL : IRegisterSL 
    {
        public readonly IRegisterRL _repositoryLayer;
        public readonly ILogger<RegisterSL> _logger;
        public readonly IConfiguration _configuration;

        private readonly string mobileRegex = @"^[2-9]{2}[0-9]{8}$";
        public readonly string passwordRegex = @"^(?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{8,}$";
        private readonly string regexMail = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public RegisterSL (IConfiguration configuration, ILogger<RegisterSL> logger, IRegisterRL RepositoryLayer)    //Dependency Injection /Constructor
        {
            _configuration = configuration;
            _logger = logger;
            _repositoryLayer = RepositoryLayer;
        }

        //User Registration
        public async Task<RegisterUserRes> RegisterUser(RegisterUserReq req)
        {
            RegisterUserRes res = new RegisterUserRes();


            try
            {
                _logger.LogInformation("Entering Service layer");
                if (String.IsNullOrEmpty(req.Email) || String.IsNullOrEmpty(req.Password) || String.IsNullOrEmpty(req.Mobile) || String.IsNullOrEmpty(req.Role) || String.IsNullOrEmpty(req.Name) || String.IsNullOrEmpty((req.DoB).ToString()) || String.IsNullOrEmpty(req.Gender) || String.IsNullOrEmpty(req.Role))
                {
                    throw new Exception(CustomExceptions.ExceptionType.Null_Empty_String_Exception.ToString());
                }

                Regex regexMailID = new Regex(regexMail);
                //   RegexOptions.CultureInvariant | RegexOptions.Singleline);
                if (!(regexMailID.IsMatch(req.Email)))
                {
                    res.Message = "Invalid Mail-ID";
                    res.IsSuccess = false;
                    return res;
                }

                Regex regexMobile = new Regex(mobileRegex);
                if (!regexMobile.IsMatch(req.Mobile))
                {
                    res.Message = "Invalid Mobile";
                    res.IsSuccess = false;
                    return res;

                }
                // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
                Regex regexPassword = new Regex(passwordRegex);
                if (!regexPassword.IsMatch(req.Password))
                {
                    res.Message = "Invalid Password: Minimum 8 characters, atleast 1 uppercase, 1 lowercase, 1 number and 1 special character";
                    res.IsSuccess = false;
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" service layer exception (Registartion):{ex}");
                res.IsSuccess = false;
                res.Message = ex.Message;
            }
            res = await _repositoryLayer.RegisterUser(req);
            return res;

        }


        //Admin Registartion
        public async Task<RegisterAdminRes> AdminRegByAdmin(RegisterAdminReq req)
        {
            RegisterAdminRes res = new RegisterAdminRes();

            try
            {
                _logger.LogInformation("Entering Service layer");
                if (String.IsNullOrEmpty(req.Email) || String.IsNullOrEmpty(req.Password) || String.IsNullOrEmpty(req.Mobile)  || String.IsNullOrEmpty(req.Name)  || String.IsNullOrEmpty(req.CreatedById))
                {
                    throw new Exception(CustomExceptions.ExceptionType.Null_Empty_String_Exception.ToString());
                }

                Regex regexMailID = new Regex(regexMail);
                //   RegexOptions.CultureInvariant | RegexOptions.Singleline);
                if (!(regexMailID.IsMatch(req.Email)))
                {
                    res.Message = "Invalid Mail-ID";
                    res.IsSuccess = false;
                    return res;
                }

                Regex regexMobile = new Regex(mobileRegex);
                if (!regexMobile.IsMatch(req.Mobile))
                {
                    res.Message = "Invalid Mobile";
                    res.IsSuccess = false;
                    return res;

                }
                // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
                Regex regexPassword = new Regex(passwordRegex);
                if (!regexPassword.IsMatch(req.Password))
                {
                    res.Message = "Invalid Password: Minimum 8 characters, atleast 1 uppercase, 1 lowercase, 1 number and 1 special character";
                    res.IsSuccess = false;
                    return res;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" service layer exception (Registartion):{ex}");
                res.IsSuccess = false;
                res.Message = ex.Message;
            }
            res = await _repositoryLayer.AdminRegByAdmin(req);
            return res;

        }
    }
}
