using Microsoft.IdentityModel.Tokens;
using SGM.Shared.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGM.Identity.Service
{
    public static class IdentityTokenService
    {
        public static string GenerateTokenFuncionario(string nome, string login, ETipoFuncionario perfil)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.NameIdentifier,login),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(ETipoFuncionario), perfil).ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("4DFF9B8EBB5314B9A62EFA72DA8B4D7658231C05")),
                                         SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string GenerateTokenUsuario(string nome, string login, ETipoUsuario perfil)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, nome),
                    new Claim(ClaimTypes.NameIdentifier,login),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(ETipoUsuario), perfil).ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("4DFF9B8EBB5314B9A62EFA72DA8B4D7658231C05")),
                                         SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
