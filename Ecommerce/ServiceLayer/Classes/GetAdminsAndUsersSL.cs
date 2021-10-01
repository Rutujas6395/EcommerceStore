using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.ServiceLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ecommerce.CommonLayer.Model.GetAdmins;
using static Ecommerce.CommonLayer.Model.GetUsers;

namespace Ecommerce.ServiceLayer.Classes
{
    public class GetAdminsAndUsersSL : IGetAdminsAndUsersSL
    {
        public readonly IGetAdminsAndUsersRL _getAdminsAndUsersRL;
        public readonly ILogger<GetAdminsAndUsersSL> _logger;
        public readonly IConfiguration _configuration;

        
        public GetAdminsAndUsersSL(IConfiguration configuration, ILogger<GetAdminsAndUsersSL> logger, IGetAdminsAndUsersRL GetAdminsAndUsersRL)    //Dependency Injection /Constructor
        {
            _configuration = configuration;
            _logger = logger;
            _getAdminsAndUsersRL = GetAdminsAndUsersRL;
        }
        public async Task<List<GetAdminsRes>> GetAdmins()
        {
            _logger.LogInformation("Entering into GetAdmins Service layer");
            var resultList = new List<GetAdminsRes>();
            resultList = await _getAdminsAndUsersRL.GetAdmins();
            return resultList;
        }
        public async Task<List<GetUsersRes>> GetUsers()
        {
            _logger.LogInformation("Entering into GetUsers Service layer");
            var resultList = new List<GetUsersRes>();
            resultList = await _getAdminsAndUsersRL.GetUsers();
            return resultList;
        }
    }
}
