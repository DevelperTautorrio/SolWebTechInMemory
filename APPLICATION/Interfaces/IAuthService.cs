using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Auth;
using APPLICATION.DTOs.Operations;

namespace APPLICATION.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto);
        Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenDto);
        Task RequestPasswordResetAsync(string email);
        Task<bool> ResetPasswordAsync(PasswordResetDto resetDto);
    }
}
