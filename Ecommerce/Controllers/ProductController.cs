using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Products;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductSL _productSL;
        public ProductController(IProductSL ProductSL)
        {
            _productSL = ProductSL;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProductDetails(AddProductReq req)
        {
            AddProductRes res = null;
            try
            {
                res = await this._productSL.AddProduct(req);
            }
            catch (Exception ex)
            {

            }
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("UpdateProduct/{DetailId}")]
        public async Task<IActionResult> UpdateProductDetails(UpdateProductReq req)
        {
            UpdateProductRes res = null;

            try
            {
                res = await this._productSL.UpdateProduct(req);
            }
            catch (Exception ex)
            {

            }
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteProduct/{DetailId}")]
        public async Task<IActionResult> DeleteEmployeeDetails(DeleteProductReq req)
        {
            DeleteProductRes res = null;

            try
            {
                res = await this._productSL.DeleteProduct(req);
            }
            catch (Exception ex)
            {

            }
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProductDetails()
        {

            var resultList = new List<GetProductsRes>();
            try
            {
                resultList = await this._productSL.GetProducts();
            }
            catch (Exception ex)
            {

            }
            return Ok(resultList);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetProduct/ProductId")]
        public async Task<IActionResult> GetProductByIdDetails(GetProductByIdReq req)
        {
            GetProductByIdRes res = null;

            try
            {
                res = await this._productSL.GetProductById(req);
            }
            catch (Exception ex)
            {

            }
            return Ok(res);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteAllProducts")]
        public async Task<IActionResult> DeleteAllProducts()
        {
            DeleteAllProductsRes res = null;
            try
            {
                res = await this._productSL.DeleteAll();
            }
            catch (Exception ex)
            {
            }
            return Ok(res);
        }

    }
}
