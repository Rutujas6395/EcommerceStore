using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.GetAdmins;
using static Ecommerce.CommonLayer.Model.GetUsers;

namespace Ecommerce.RepositoryLayer.Classes
{
    public class GetAdminsAndUsersRL :IGetAdminsAndUsersRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public readonly ILogger<GetAdminsAndUsersRL> _logger;
        private SqlConnection sqlConnectionVariable;
        const int ConnectionTimeout = 180;
        int status = 0;

        public GetAdminsAndUsersRL(ILogger<GetAdminsAndUsersRL> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            this.sqlConnectionVariable = new SqlConnection(_configuration["ConnectionStrings:DatabaseConnectionString"]);
            //  this.sqlConnection = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DatabaseConnectionString").Value);
        }

        //GetAdmins
        public async Task<List<GetAdminsRes>> GetAdmins()
        {
            var resultList = new List<GetAdminsRes>();

            try
            {
                _logger.LogInformation("Entering into GetAdmins repository layer");
                SqlCommand sqlCommand = new SqlCommand("sp_getAdmins", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlConnectionVariable.Open();
                DbDataReader db = await sqlCommand.ExecuteReaderAsync();

                if (db.HasRows)
                {
                    while (await db.ReadAsync())

                        resultList.Add(new GetAdminsRes()
                        {
                            IsSuccess = true,
                            AdminId= db["AdminId"] != DBNull.Value ? Convert.ToInt32(db["AdminId"]) : 0,
                            Name = db["Name"] != DBNull.Value ? (db["Name"]).ToString() : null,
                            Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null,
                            Mobile = db["Mobile"] != DBNull.Value ? (db["Mobile"]).ToString() : null,
                            CreatedById = db["CreatedById"] != DBNull.Value ? Convert.ToInt32(db["CreatedById"]) : 0,
                            DoC = db["DoC"] != DBNull.Value ? Convert.ToDateTime(db["DoC"]) : default,
                            Message = "Successful"

                        });
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnectionVariable.Close();
            }

            return resultList;
        }


        //GetUsers
        public async Task<List<GetUsersRes>> GetUsers()
        {
            var resultList = new List<GetUsersRes>();

            try
            {
                _logger.LogInformation("Entering into GetUsers repository layer");
                SqlCommand sqlCommand = new SqlCommand("sp_GetUsers", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
               //sqlCommand.Parameters.AddWithValue("@Role", "User");
                sqlConnectionVariable.Open();

                DbDataReader db = await sqlCommand.ExecuteReaderAsync();
                if (db.HasRows)
                {
                    while (await db.ReadAsync())

                        resultList.Add(new GetUsersRes()
                        {
                            IsSuccess = true,
                            UserId = db["UserId"] != DBNull.Value ? Convert.ToInt32(db["UserId"]) : 0,
                            Name = db["Name"] != DBNull.Value ? (db["Name"]).ToString() : null,
                            Email = db["Email"] != DBNull.Value ? db["Email"].ToString() : null,
                            Mobile = db["Mobile"] != DBNull.Value ? (db["Mobile"]).ToString() : null,
                            Gender = db["Gender"] != DBNull.Value ? (db["Gender"]).ToString() : null,
                            DoB = db["DoB"] != DBNull.Value ? Convert.ToDateTime(db["DoB"]) : default,
                            Age = db["Age"] != DBNull.Value ? Convert.ToInt32(db["Age"]) : 0,
                            IsActive = db["IsActive"] != DBNull.Value ? Convert.ToBoolean((db["IsActive"])) : false,
                            Message = "Successful"

                        });
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sqlConnectionVariable.Close();
            }

            return resultList;
        }
    }
}
