using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Login;

namespace Ecommerce.ServiceLayer.Interfaces
{
   public interface ILoginSL
    {
        Task<LoginRes> Login(LoginReq req);
    }
}
