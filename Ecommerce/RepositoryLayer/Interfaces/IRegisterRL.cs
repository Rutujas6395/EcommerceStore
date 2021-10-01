﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.AdminRegByAdmin;
using static Ecommerce.CommonLayer.Model.RegisterUser;

namespace Ecommerce.RepositoryLayer.Interfaces
{
    public interface IRegisterRL
    {
        Task<RegisterUserRes> RegisterUser(RegisterUserReq req);
        Task<RegisterAdminRes> AdminRegByAdmin(RegisterAdminReq req);
    }
}
