using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CommonLayer.Model
{
    public class AdminRegByAdmin
    {
        public class RegisterAdminReq
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Password { get; set; }
            public string CreatedById { get; set; }
        }
        public class RegisterAdminRes
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
            public string Password { get; set; }
            public string CreatedById { get; set; }
            public DateTime DoC { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }
    }
}
