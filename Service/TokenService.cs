using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Videojuegos.Repositories;

namespace Videojuegos.Services
{
    public class TokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly string _jwtSecretKey;

        public TokenService(ITokenRepository tokenRepository, string jwtSecretKey)
        {
            _tokenRepository = tokenRepository;
            _jwtSecretKey = jwtSecretKey;
        }

        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto authRequest)
        {
            var usuario = await _tokenRepository.LoginAsync(authRequest);

            if (usuario == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Email, usuario.Email ?? "")
            };

            foreach (var rol in usuario.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol.Nombre));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return new LoginResponseDto
            {
                Token = tokenString,
                Usuario = usuario
            };
        }

    }
}
