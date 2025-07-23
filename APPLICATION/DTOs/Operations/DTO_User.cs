using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.DTOs.Operations
{
    
    public class PasswordResetDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100, MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }

    
    public class EmailRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
