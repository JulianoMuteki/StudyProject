using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StudyProject.Secutity
{
    public static class CustomToken
    {
        public static UserToken GenerateToken(string userUniqueName, string jwtKey, string tokenExpireHours, string tokenIssuer, string tokenAudience, IList<Claim> userClaims)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userUniqueName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.AddRange(userClaims);
            //// Add roles as multiple claims
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey));
            //TODO: check HmacSha256Signature
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(double.Parse(tokenExpireHours));

            JwtSecurityToken token = new JwtSecurityToken(
              issuer: tokenIssuer,
              audience: tokenAudience,
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            return new UserToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };
        }
    }
}
