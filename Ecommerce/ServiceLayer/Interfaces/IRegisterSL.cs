using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.AdminRegByAdmin;
using static Ecommerce.CommonLayer.Model.RegisterUser;

namespace Ecommerce.ServiceLayer.Interfaces
{
    public interface IRegisterSL
    {
        Task<RegisterUserRes> RegisterUser(RegisterUserReq req);
        Task<RegisterAdminRes> AdminRegByAdmin(RegisterAdminReq req);
    }

}
