using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.GetAdmins;
using static Ecommerce.CommonLayer.Model.GetUsers;

namespace Ecommerce.RepositoryLayer.Interfaces
{
    public interface IGetAdminsAndUsersRL
    {
        Task<List<GetAdminsRes>> GetAdmins();
        Task<List<GetUsersRes>> GetUsers();
    }
}
