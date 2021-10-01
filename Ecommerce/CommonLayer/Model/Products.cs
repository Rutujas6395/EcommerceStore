using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CommonLayer.Model
{
    public class Products
    {
        //==========  Add Product================
        public class AddProductReq
        {
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string Color { get; set; }
            public string Dimentions { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public int CreatedByAdminId { get; set; }
            public string Images { get; set; }
            public bool IsAvailable { get; set; }
        }
        public class AddProductRes
        {
            public bool IsSuccess { get; set; }
        }

        //============ Update Product  =================
        public class UpdateProductReq
        {
            public int DetailId { get; set; }
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string Color { get; set; }
            public string Dimentions { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public int CreatedByAdminId { get; set; }
            public string Images { get; set; }
            public bool IsAvailable { get; set; }

        }
        public class UpdateProductRes
        {
            public bool IsUpdateSuccess { get; set; }
        }

        //=============  Delete Product =============
        public class DeleteProductReq
        {
            public int DetailId { get; set; }
        }
        public class DeleteProductRes
        {
            public bool IsDeleteSuccess { get; set; }
        }

        // =========  Get All Products   =====================

        public class GetProductsRes
        {
            public int DetailId { get; set; }
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string Color { get; set; }
            public string Dimentions { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public int CreatedByAdminId { get; set; }
            public string Images { get; set; }
            public bool IsAvailable { get; set; }
            public bool IsSuccess { get; set; }
     
        }



        //========== Get Product by ID =================
        public class GetProductByIdReq
        {
            public int DetailId { get; set; }
        }
        public class GetProductByIdRes
        {
            public int DetailId { get; set; }
            public string ProductName { get; set; }
            public string Brand { get; set; }
            public string Color { get; set; }
            public string Dimentions { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public int CreatedByAdminId { get; set; }
            public string Images { get; set; }
            public bool IsAvailable { get; set; }
            public bool IsGetProdByIdSuccess { get; set; }
        }
        //========  Delete All Products ============
        public class DeleteAllProductsRes
        {
            public bool IsSuccess { get; set; }

        }
    }
}
