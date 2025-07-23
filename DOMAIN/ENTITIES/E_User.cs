using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.ENTITIES
{
    public class E_User
    {
        public string FirstName { get; set; }
        public string PaternalSurname { get; set; }
        public string? MaternalSurname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastConnection { get; set; }
        public bool Active { get; set; } = true;
        public string? RecoveryToken { get; set; }
        public DateTime? TokenExpiration { get; set; }
        public string? Phone { get; set; }
        public string? Biography { get; set; }
        public string UserRole { get; set; } = "User";
    }
}

