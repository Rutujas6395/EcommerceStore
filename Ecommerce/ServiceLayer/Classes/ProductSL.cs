using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Products;

namespace Ecommerce.ServiceLayer.Classes
{
    public class ProductSL : IProductSL
    {
        public readonly IProductRL _productRL;

        public ProductSL(IProductRL ProductRL)    //Defination Injection /Constructor
        {
            _productRL = ProductRL;
        }

        public async Task<AddProductRes> AddProduct(AddProductReq req)
        {
            AddProductRes res = null;
            res = await _productRL.AddProduct(req);
            return res;
        }
        public async Task<UpdateProductRes> UpdateProduct(UpdateProductReq req)
        {
            UpdateProductRes res = null;
            res = await _productRL.UpdateProduct(req);
            return res;
        }
        public async Task<DeleteProductRes> DeleteProduct(DeleteProductReq req)
        {
            DeleteProductRes res = null;
            res = await _productRL.DeleteProduct(req);
            return res;
        }

        public async Task<List<GetProductsRes>> GetProducts()
        {
            var resultList = new List<GetProductsRes>();
            resultList = await _productRL.GetProducts();
            return resultList;
        }

        public async Task<GetProductByIdRes> GetProductById(GetProductByIdReq req)
        {
            GetProductByIdRes res = null;
            res = await _productRL.GetProductById(req);
            return res;
        }
        public async Task<DeleteAllProductsRes> DeleteAll()
        {//DeleteAllRes
            DeleteAllProductsRes res = null;
            res = await _productRL.DeleteAll();
            return res;
        }

    }
}
