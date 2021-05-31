using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;


namespace core.util.Middleware
{
    public static class TokenGenerator
    {
        public static string GenerateTokenJwt(string username, string uid, string role)
        {

            var secretKey = "JWT_SECRET_KEY".GetValueAppSetting<string>();
            var audienceToken = "JWT_AUDIENCE_TOKEN".GetValueAppSetting<string>();
            var issuerToken = "JWT_ISSUER_TOKEN".GetValueAppSetting<string>();
            var expireTime = "JWT_EXPIRE_MINUTES".GetValueAppSetting<string>();

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);


            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, uid),
                    new Claim(ClaimTypes.Role, role??string.Empty)
                });


            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}