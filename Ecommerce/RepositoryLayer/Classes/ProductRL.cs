using Ecommerce.RepositoryLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Products;

namespace Ecommerce.RepositoryLayer.Classes
{
    public class ProductRL : IProductRL
    {
        public readonly IConfiguration _configuration;
        public readonly SqlConnection sqlConnection;
        public readonly ILogger<ProductRL> _logger;
        private SqlConnection sqlConnectionVariable;
        const int ConnectionTimeout = 180;

        public ProductRL(ILogger<ProductRL> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
            this.sqlConnectionVariable = new SqlConnection(_configuration["ConnectionStrings:DatabaseConnectionString"]);
        }
    

        //       Adding Product
        public async Task<AddProductRes> AddProduct(AddProductReq req)
        {
            string Password = string.Empty;
            AddProductRes res = new AddProductRes()
            {
                IsSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Add");
                sqlCommand.Parameters.AddWithValue("@ProductName", req.ProductName);
                sqlCommand.Parameters.AddWithValue("@Brand", req.Brand);
                sqlCommand.Parameters.AddWithValue("@Color", req.Color);
                sqlCommand.Parameters.AddWithValue("@Dimentions", req.Dimentions);
                sqlCommand.Parameters.AddWithValue("@Category", req.Category);
                sqlCommand.Parameters.AddWithValue("@Price", req.Price);
                sqlCommand.Parameters.AddWithValue("@CreatedByAdminId", req.CreatedByAdminId);
                sqlCommand.Parameters.AddWithValue("@Images", req.Images);
              
                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    res.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Adding Product):{ex}");
                res.IsSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;
        }


        //    Update Product
        public async Task<UpdateProductRes> UpdateProduct(UpdateProductReq req)
        {
            UpdateProductRes res = new UpdateProductRes()
            {
                IsUpdateSuccess = true
            };

            try
            {  _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Update");
                sqlCommand.Parameters.AddWithValue("@ProductName", req.ProductName);
                sqlCommand.Parameters.AddWithValue("@Brand", req.Brand);
                sqlCommand.Parameters.AddWithValue("@Color", req.Color);
                sqlCommand.Parameters.AddWithValue("@Dimentions", req.Dimentions);
                sqlCommand.Parameters.AddWithValue("@Category", req.Category);
                sqlCommand.Parameters.AddWithValue("@Price", req.Price);
                sqlCommand.Parameters.AddWithValue("@CreatedByAdminId", req.CreatedByAdminId);
                sqlCommand.Parameters.AddWithValue("@Images", req.Images);
                sqlCommand.Parameters.AddWithValue("@DoM", (DateTime.Now).ToString());
                sqlCommand.Parameters.AddWithValue("@DetailId", req.DetailId);
                sqlCommand.Parameters.AddWithValue("@IsAvailable", req.IsAvailable);
                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    res.IsUpdateSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                res.IsUpdateSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;
        }



        //    Delete BYID Product
        public async Task<DeleteProductRes> DeleteProduct(DeleteProductReq req)
        {
            DeleteProductRes res = new DeleteProductRes()
            {
                IsDeleteSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "DeleteById");
                sqlCommand.Parameters.AddWithValue("@DetailId", req.DetailId);
                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                // bool status =(res.IsDeleteSuccess);
                if (status <= 0)
                {
                    res.IsDeleteSuccess = false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                res.IsDeleteSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;
        }

        //DeleteAll
        public async Task<DeleteAllProductsRes> DeleteAll()
        {
            DeleteAllProductsRes res = new DeleteAllProductsRes()
            {
                IsSuccess = true
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "DeleteAll");
                sqlConnectionVariable.Open();
                int status = await sqlCommand.ExecuteNonQueryAsync();
                if (status <= 0)
                {
                    res.IsSuccess = false;
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                res.IsSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;

        }




        //    Get All Product list
        public async Task<List<GetProductsRes>> GetProducts()
        {
            var resultList = new List<GetProductsRes>();
          
            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Get All Product list) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "GetAll");
                sqlConnectionVariable.Open();

                using (DbDataReader db = await sqlCommand.ExecuteReaderAsync())
                {
                    if (db.HasRows)
                    {
                        while (await db.ReadAsync())

                            resultList.Add(new GetProductsRes()
                            {
                                DetailId = db["DetailId"] != DBNull.Value ? Convert.ToInt32(db["DetailId"]) : 0,
                                ProductName = db["ProductName"] != DBNull.Value ? (db["ProductName"]).ToString() : null,
                                Brand = db["Brand"] != DBNull.Value ? (db["Brand"]).ToString() : null,
                                Color = db["Color"] != DBNull.Value ? (db["Color"]).ToString() : null,
                                Dimentions = db["Dimentions"] != DBNull.Value ? (db["Dimentions"]).ToString() : null,
                                Category = db["Category"] != DBNull.Value ? (db["Category"]).ToString() : null,
                                Price = db["Price"] != DBNull.Value ? Convert.ToDouble((db["Price"])) : 0.0,
                                CreatedByAdminId = db["CreatedByAdminId"] != DBNull.Value ? Convert.ToInt32(db["CreatedByAdminId"]) : 0,
                                Images = db["Images"] != DBNull.Value ? (db["Images"]).ToString() : null,
                                IsAvailable = db["IsAvailable"] != DBNull.Value ? Convert.ToBoolean(db["IsAvailable"]) : false,
                                IsSuccess = true
                            }) ; 

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
            }
            finally
            {
                sqlConnectionVariable.Close();
            }

            return resultList;
        }

        //    GetProductBYID
        public async Task<GetProductByIdRes> GetProductById(GetProductByIdReq req)
        {
            GetProductByIdRes res = new GetProductByIdRes()
            {
                IsGetProdByIdSuccess = true,
                DetailId = 0,
                ProductName = null,
                Brand = null,
                Color = null,
                Dimentions = null,
                Category = null,
                Price = 0,
                CreatedByAdminId = 0,
                Images=null,
                IsAvailable=false
            };

            try
            {
                _logger.LogInformation("Enter in Repository Layer of (Login) ");
                SqlCommand sqlCommand = new SqlCommand("sp_Products", this.sqlConnectionVariable);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = ConnectionTimeout;
                sqlCommand.Parameters.AddWithValue("@StatementType", "GetId");
                sqlCommand.Parameters.AddWithValue("@DetailId", req.DetailId);
                sqlConnectionVariable.Open();
                using (DbDataReader dc = await sqlCommand.ExecuteReaderAsync())
                {
                    if (dc.HasRows)
                    {
                        await dc.ReadAsync();
                        res.DetailId = dc["DetailId"] != DBNull.Value ? Convert.ToInt32(dc["DetailId"]) : 0;
                        res.ProductName = dc["ProductName"] != DBNull.Value ? (dc["ProductName"]).ToString() : null;
                        res.Brand = dc["Brand"] != DBNull.Value ? (dc["Brand"]).ToString() : null;
                        res.Color = dc["Color"] != DBNull.Value ? (dc["Color"]).ToString() : null;
                        res.Dimentions = dc["Dimentions"] != DBNull.Value ? (dc["Dimentions"]).ToString() : null;
                        res.Category = dc["Category"] != DBNull.Value ? (dc["Category"]).ToString() : null;
                        res.Price = dc["Price"] != DBNull.Value ? Convert.ToDouble(dc["Price"]) : 0;
                        res.CreatedByAdminId = dc["CreatedByAdminId"] != DBNull.Value ? Convert.ToInt32(dc["CreatedByAdminId"]) : 0;
                        res.Images = dc["Images"] != DBNull.Value ? (dc["Images"]).ToString() : null;
                        res.IsAvailable= dc["IsAvailable"] != DBNull.Value ? Convert.ToBoolean(dc["IsAvailable"]):false;
                      }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Repository layer exception (Updating Product):{ex}");
                res.IsGetProdByIdSuccess = false;
            }
            finally
            {
                sqlConnectionVariable.Close();
            }
            return res;
        }

   }
}
