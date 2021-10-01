using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Products;

namespace Ecommerce.ServiceLayer.Interfaces
{
    public interface IProductSL
    {
        Task<AddProductRes> AddProduct(AddProductReq req);
        Task<UpdateProductRes> UpdateProduct(UpdateProductReq req);
        Task<DeleteProductRes> DeleteProduct(DeleteProductReq req);
        Task<List<GetProductsRes>> GetProducts();
        Task<GetProductByIdRes> GetProductById(GetProductByIdReq req);
        Task<DeleteAllProductsRes> DeleteAll();
    }
}
