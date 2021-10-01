using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.CommonLayer.Model
{
    public class GetAdmins
    {
        public class GetAdminsRes
        {
            public int AdminId { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public int CreatedById { get; set; }
            public DateTime DoC { get; set; }
            public string Message { get; set; }
            public bool IsSuccess { get; internal set; }
        }
    }
}
