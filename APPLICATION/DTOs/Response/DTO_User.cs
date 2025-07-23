using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.DTOs.Response
{
    public class UserResponseDto
    {
        public int UserID { get; set; }
        public string FullName { get; set; } 
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string? Biography { get; set; }
    }

    
    public class UserDetailDto : UserResponseDto
    {
        public DateTime? BirthDate { get; set; }
        public DateTime? LastConnection { get; set; }
        public string? Phone { get; set; }
    }
}
