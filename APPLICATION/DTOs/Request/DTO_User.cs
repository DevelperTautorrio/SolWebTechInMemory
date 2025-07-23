using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.DTOs.Request
{
   
    public class UserCreateDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty; 

        [Required, StringLength(100)]
        public string PaternalSurname { get; set; } = string.Empty; 

        [StringLength(100)]
        public string? MaternalSurname { get; set; } 

        [EmailAddress, Required, StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9_]+$")] 
        public string Nickname { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [Url, StringLength(500)]
        public string? ProfilePicture { get; set; } 

        [Phone, StringLength(20)]
        public string? Phone { get; set; } 
    }

    
    public class UserUpdateDto
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? PaternalSurname { get; set; }

        [Url, StringLength(500)]
        public string? ProfilePicture { get; set; }

        [Phone, StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(1000)]
        public string? Biography { get; set; }
    }
}
