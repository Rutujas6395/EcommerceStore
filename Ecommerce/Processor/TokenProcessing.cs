using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Registration.Processor
{
    public class TokenProcessing
    {
        public readonly IConfiguration configuration;
        public TokenProcessing(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public static string CreateToken(string Email, string Role,string Key, string Issuer)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("Email", Email));
                claims.Add(new Claim(ClaimTypes.Role, Role ));
               
                var token = new JwtSecurityToken(Issuer,
                    Issuer,
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

