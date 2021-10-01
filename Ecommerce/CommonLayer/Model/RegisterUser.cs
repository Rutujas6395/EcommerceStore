using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CommonLayer.Model
{
    public class RegisterUser
    {
        //Register new Employee
        public class RegisterUserReq
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Gender{ get; set; }
            public DateTime DoB{ get; set; }
            //public DateTime DoC { get; set; }
            public int Age { get; set; }
            public string Role { get; set; }
           
        }
        public class RegisterUserRes
        {
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string Password { get; set; }
            public DateTime DoB { get; set; }
            public string Role { get; set; }
            public int Age { get; set; }
           // public bool IsActive { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

    }
}
