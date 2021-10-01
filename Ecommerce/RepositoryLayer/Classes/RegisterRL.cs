using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Registration.EncryptDecryptProcessor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static Ecommerce.CommonLayer.Model.RegisterUser;
using Ecommerce.RepositoryLayer.Interfaces;
using static Ecommerce.CommonLayer.Model.AdminRegByAdmin;

namespace Ecommerce.RepositoryLayer.Classes
{ public class RegisterRL :IRegisterRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public readonly ILogger<RegisterRL> _logger;
        private SqlConnection sqlConnectionVariable;
        const int ConnectionTimeout = 180;
        int status = 0;

        public RegisterRL(ILogger<RegisterRL> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            this.sqlConnectionVariable = new SqlConnection(_configuration["ConnectionStrings:DatabaseConnectionString"]);
            //  this.sqlConnection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DatabaseConnectionString").Value);
        }

         //==============     Adding User      =========================
        public async Task<RegisterUserRes> RegisterUser(RegisterUserReq req)
        {
            string Password = string.Empty;
            RegisterUserRes res = new RegisterUserRes()
            {
                IsSuccess = true
            };

            try
            {
                if (sqlConnectionVariable != null)
                {
                    _logger.LogInformation("Enter in Repository Layer(Registartion) ");
                    SqlCommand sqlCommand = new SqlCommand("sp_RegisterUser", this.sqlConnectionVariable);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeout;
                    sqlCommand.Parameters.AddWithValue("@Name", req.Name);
                    sqlCommand.Parameters.AddWithValue("@Mobile", req.Mobile);
                    sqlCommand.Parameters.AddWithValue("@Email", req.Email);
                    sqlCommand.Parameters.AddWithValue("@Gender", req.Gender);
                    Password = PasswordProcessing.Encrypt(req.Password.ToString(), _configuration["SecurityKey"]);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@DoB", req.DoB);
                    sqlCommand.Parameters.AddWithValue("@Age", req.Age);
                    sqlCommand.Parameters.AddWithValue("@Role", req.Role);
                    //sqlCommand.Parameters.AddWithValue("@IsActive", (req.IsActive));
                    sqlConnectionVariable.Open();

                    status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        res.IsSuccess = false;
                    }
                    else
                    {
                        res.Name = req.Name;
                        res.Email = req.Email;
                        res.Password = Password;
                        res.Mobile = req.Mobile;
                        res.Gender = req.Gender;
                        res.DoB = req.DoB;
                        res.Age = req.Age;
                        res.Role = req.Role;
                        res.Message = " Registered Successfully!";
                       // res.IsActive = req.IsActive;
                    }
                }
                else
                {
                    res.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" Repository layer exception (Registartion):{ex}");
                res.IsSuccess = false;

            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;
        }

        //==============     Adding Admin      =========================
        public async Task<RegisterAdminRes> AdminRegByAdmin(RegisterAdminReq req)
        {
            string Password = string.Empty;
            RegisterAdminRes res = new RegisterAdminRes()
            {
                IsSuccess = true
            };

            try
            {
                if (sqlConnectionVariable != null)
                {
                    _logger.LogInformation("Enter in Repository Layer(Registartion) ");
                    SqlCommand sqlCommand = new SqlCommand("sp_RegisterAdmin", this.sqlConnectionVariable);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = ConnectionTimeout;
                    sqlCommand.Parameters.AddWithValue("@Name", req.Name);
                    sqlCommand.Parameters.AddWithValue("@Mobile", req.Mobile);
                    sqlCommand.Parameters.AddWithValue("@Email", req.Email);
                    Password = PasswordProcessing.Encrypt(req.Password.ToString(), _configuration["SecurityKey"]);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@CreatedById", req.CreatedById);   //======= HOw to Pass the Id OF Craeted Admin
                    sqlConnectionVariable.Open();

                    status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        res.IsSuccess = false;
                    }
                    else
                    {
                        res.Name = req.Name;
                        res.Email = req.Email;
                        res.Password = Password;
                        res.Mobile = req.Mobile;
                        res.CreatedById = res.CreatedById;
                        res.Message = " Registered Successfully! As Password is generated by ADMIN ...Kindly Change it for Security!";
                     }
                }
                else
                {
                    res.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" Repository layer exception (Registartion):{ex}");
                res.IsSuccess = false;

            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;
        }

    }
}
