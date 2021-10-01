using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CommonLayer.Model
{
    public class Login
    {
        public class LoginReq
        { 
          
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }


        }
        public class LoginRes
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public bool IsSuccess { get; set; }
            public string Role { get; set; }
            public string Token { get; set; }
        }
    }
}
