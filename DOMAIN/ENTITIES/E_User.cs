using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.ENTITIES
{
    public class E_User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Paternal surname is required")]
        [StringLength(100, ErrorMessage = "Paternal surname cannot exceed 100 characters")]
        public string? PaternalSurname { get; set; }

        [StringLength(100, ErrorMessage = "Maternal surname cannot exceed 100 characters")]
        public string? MaternalSurname { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nickname is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Nickname can only contain letters, numbers and underscores")]
        public string Nickname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password hash is required")]
        [StringLength(255, ErrorMessage = "Password hash cannot exceed 255 characters")]
        public string? PasswordHash { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [StringLength(500, ErrorMessage = "Profile picture URL cannot exceed 500 characters")]
        public string? ProfilePicture { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LastConnection { get; set; }

        [StringLength(255, ErrorMessage = "Recovery token cannot exceed 255 characters")]
        public string? RecoveryToken { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? TokenExpiration { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? Phone { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters")]
        public string? Biography { get; set; }
    }
}

