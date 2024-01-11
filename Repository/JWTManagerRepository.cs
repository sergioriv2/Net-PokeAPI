﻿using Microsoft.IdentityModel.Tokens;
using PokeApi.Interfaces;
using PokeApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PokeApi.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration _configuration;

        public JWTManagerRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Tokens GenerateTokens(string username)
        {
            try {
                var date = DateTime.UtcNow;
                var tokenHandler = new JwtSecurityTokenHandler();

                var accessTokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:AccessKey"]);
                var refreshTokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:RefreshKey"]);

                var accessTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                                new Claim("username", username)
                            }
                        ),
                    Expires = date.AddMinutes(1),
                    NotBefore = date,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(accessTokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var refreshTokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                                new Claim("username", username)
                            }
                        ),
                    Expires = date.AddDays(7),
                    NotBefore = date,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(refreshTokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var accessToken = tokenHandler.CreateToken(accessTokenDescriptor);
                var refreshToken = tokenHandler.CreateToken(refreshTokenDescriptor);

                return new Tokens()
                {
                    AccessToken = tokenHandler.WriteToken(accessToken),
                    RefreshToken = tokenHandler.WriteToken(refreshToken),
                };

            }
            catch (Exception ex) { 
                System.Console.WriteLine(ex);
                return null;
            }
        }

        public string GenerateRefreshToken() { 
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public Tokens GenerateJWT(string username)
        {
            return GenerateTokens(username);
        }

        public Tokens GenerateRefreshToken(string username)
        {
            return GenerateTokens(username);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out SecurityToken securityToken
            );

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
