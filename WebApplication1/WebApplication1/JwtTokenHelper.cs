using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;

namespace WebApplication1
{
    public static class JwtTokenHelper
    {
        public static string GetToken(List<Claim> claims)
        {
            var token = new JwtSecurityToken
            (
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims: claims,
                signingCredentials: CreateSigningCredentials(),
                expires: DateTime.UtcNow.AddYears(2) // changing this to what ever you want the token to be valid
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private static SigningCredentials CreateSigningCredentials()
        {
            var securityKey = new InMemorySymmetricSecurityKey(JwtSettings.Secret);
          
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature,
                SecurityAlgorithms.Sha256Digest
            );

            return signingCredentials;
        }
    }
}