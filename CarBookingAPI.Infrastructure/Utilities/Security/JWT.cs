using CarBookingAPI.Infrastructure.Constants.Security;
using CarBookingAPI.Infrastructure.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CarBookingAPI.Infrastructure.Utilities.Security
{
    public class JWT
    {
        private string _jwtSecret;
        public JWT(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }

        public string CreateAccessToken(UsersViewModel user, ICollection<string> roles)
        {
            if (user != null && roles != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken
                (
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddMinutes(15),
                    issuer: JWTConstants.AccessIssuer,
                    audience: JWTConstants.Audience
                );

                token.SigningKey = key;

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        public string CreateRefreshToken(UsersViewModel viewModel, ICollection<string> roles)
        {
            if (viewModel != default)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, viewModel.Username),
                    new Claim(ClaimTypes.Sid, viewModel.Id.ToString())
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                //Credential used to sign JWT Signature
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken
                (
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddDays(7),
                    issuer: JWTConstants.RefreshIssuer,
                    audience: JWTConstants.Audience
                );

                token.SigningKey = key;

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        public string RecreateAccessToken(string accessToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken
                (
                    claims: ReadClaimsFromToken(accessToken),
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddSeconds(900),
                    issuer: JWTConstants.AccessIssuer,
                    audience: JWTConstants.Audience
                );

                token.SigningKey = key;

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        public string RecreateRefreshToken(string refreshToken)
        {
            if (!string.IsNullOrEmpty(refreshToken))
            {
                if (CheckRefreshToken(refreshToken) == refreshToken)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                    var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                    var token = new JwtSecurityToken
                    (
                        claims: ReadClaimsFromToken(refreshToken),
                        signingCredentials: signInCredentials,
                        expires: DateTime.Now.AddDays(7),
                        issuer: JWTConstants.RefreshIssuer,
                        audience: JWTConstants.Audience
                    );

                    token.SigningKey = key;

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }

                return null;
            }

            return null;
        }

        private string CheckRefreshToken(string refreshToken)
        {
            var expirationTime = ReadExpirationTime(refreshToken);

            if (expirationTime != default && expirationTime > DateTime.Now)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
                var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken
                (
                    claims: ReadClaimsFromToken(refreshToken),
                    signingCredentials: signingCredentials,
                    expires: expirationTime,
                    issuer: JWTConstants.RefreshIssuer,
                    audience: JWTConstants.Audience
                );

                token.SigningKey = key;

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            return null;
        }

        public ICollection<Claim> ReadClaimsFromToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return new JwtSecurityTokenHandler()
                    .ReadJwtToken(token)
                    .Claims
                    .ToList();
            }

            return null;
        }

        public string ReadUsername(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return new JwtSecurityToken(token)
                    .Claims
                    .Where(x => x.Type == ClaimTypes.Name)
                    .Select(x => x.Value)
                    .FirstOrDefault();
            }

            return null;
        }

        private DateTime? ReadExpirationTime(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                return new JwtSecurityToken(token).ValidTo;
            }

            return null;
        }
    }
}
