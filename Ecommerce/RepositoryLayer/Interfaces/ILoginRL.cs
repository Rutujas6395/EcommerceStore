using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.Login;

namespace Ecommerce.RepositoryLayer.Interfaces
{
   public interface ILoginRL
    {
        Task<LoginRes> Login(LoginReq req);
    }
}
