using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Response;

namespace APPLICATION.DTOs.Auth
{

    
    public class LoginRequestDto
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }

    
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public UserResponseDto User { get; set; }
    }

    
    public class RefreshTokenRequestDto
    {
        [Required]
        public string ExpiredToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }




}
