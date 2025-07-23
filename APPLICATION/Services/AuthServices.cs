using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Auth;
using APPLICATION.DTOs.Operations;
using APPLICATION.DTOs.Response;
using APPLICATION.Interfaces;
using APPLICATION.Models;
using AutoMapper;
using DOMAIN.ENTITIES;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace APPLICATION.Services
{
    public class AuthServices : IAuthService
    {
        private readonly B_User _businessLayer;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthServices(B_User businessLayer, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _businessLayer = businessLayer;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _businessLayer.GetByEmail(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                User = _mapper.Map<UserResponseDto>(user)
            };
        }

        private string GenerateJwtToken(E_User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Nickname)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenDto)
        {
            
            throw new NotImplementedException();
        }

        public Task RequestPasswordResetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(PasswordResetDto resetDto)
        {
            throw new NotImplementedException();
        }
    }
}
