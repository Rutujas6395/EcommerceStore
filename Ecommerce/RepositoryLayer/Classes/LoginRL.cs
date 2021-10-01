using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Registration.EncryptDecryptProcessor;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Login;

namespace Ecommerce.RepositoryLayer.Classes
{
    public class LoginRL : ILoginRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public readonly ILogger<LoginRL> _logger;
        private SqlConnection sqlConnectionVariable;
        const int ConnectionTimeout = 180;
        int status = 0;

        public LoginRL(ILogger<LoginRL> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            this.sqlConnectionVariable = new SqlConnection(_configuration["ConnectionStrings:DatabaseConnectionString"]);
            //  this.sqlConnection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DatabaseConnectionString").Value);
        }

        public async Task<LoginRes> Login(LoginReq req)
        {
            string Password = string.Empty;
            LoginRes res = new LoginRes()
            {
                IsSuccess = true,
                Email = null,
                Role = null
            };

            try
            {
                if(req.Role == "Admin")
                {
                    if (sqlConnectionVariable != null)
                    {
                        _logger.LogInformation("Enter in Repository Layer of (Login) ");
                        SqlCommand sqlCommand = new SqlCommand("sp_Login", this.sqlConnectionVariable);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeout;
                        sqlCommand.Parameters.AddWithValue("@StatementType", req.Role);
                        sqlCommand.Parameters.AddWithValue("@Email", req.Email);
                        //sqlCommand.Parameters.AddWithValue("@Role", req.Role);
                        Password = PasswordProcessing.Encrypt(req.Password.ToString(), _configuration["SecurityKey"]);
                        sqlCommand.Parameters.AddWithValue("@Password", Password);
                        sqlConnectionVariable.Open();
                        DbDataReader db = await sqlCommand.ExecuteReaderAsync();

                        if (db.HasRows)
                        {
                            await db.ReadAsync();
                            res.Id = db["AdminId"] != DBNull.Value ? Convert.ToInt32(db["AdminId"]) : 0;
                            res.Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null;
                            res.Role = "Admin";
                           // res.IsActive = db["IsActive"] != DBNull.Value ? Convert.ToBoolean(db["IsActive"]) : true;
                        }
                        else
                        {
                            res.IsSuccess = false;
                        }
                    }
                    else
                    {
                        res.IsSuccess = false;
                    }
                }
                else //UserLogin 
                {
                    if (sqlConnectionVariable != null)
                    {
                        _logger.LogInformation("Enter in Repository Layer of (Login) ");
                        SqlCommand sqlCommand = new SqlCommand("sp_Login", this.sqlConnectionVariable);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandTimeout = ConnectionTimeout;
                        sqlCommand.Parameters.AddWithValue("@StatementType", req.Role);
                        sqlCommand.Parameters.AddWithValue("@Email", req.Email);
                        //sqlCommand.Parameters.AddWithValue("@Role", req.Role);
                        Password = PasswordProcessing.Encrypt(req.Password.ToString(), _configuration["SecurityKey"]);
                        sqlCommand.Parameters.AddWithValue("@Password", Password);
                        sqlConnectionVariable.Open();
                        DbDataReader db = await sqlCommand.ExecuteReaderAsync();

                        if (db.HasRows)
                        {
                            await db.ReadAsync();
                            res.Id = db["UserId"] != DBNull.Value ? Convert.ToInt32(db["UserId"]) : 0;
                            res.Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null;
                            res.Role = req.Role;
                            //res.IsActive = db["IsActive"] != DBNull.Value ? Convert.ToBoolean(db["IsActive"]) : true;
                        }
                        else
                        {
                            res.IsSuccess = false;
                        }
                    }
                    else
                    {
                        res.IsSuccess = false;
                    }


                }
            }
                
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Registartion):{ex}");
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
