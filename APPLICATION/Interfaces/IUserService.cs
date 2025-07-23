using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Request;
using APPLICATION.DTOs.Response;

namespace APPLICATION.Interfaces
{
    public interface IUserService
    {
        
        Task<UserResponseDto> GetUserByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<int> CreateUserAsync(UserCreateDto userDto);
        Task UpdateUserAsync(int id, UserUpdateDto userDto);
        Task DeleteUserAsync(int id);

        
        Task<UserResponseDto> GetUserByEmailAsync(string email);
        Task UpdateUserProfilePictureAsync(int userId, string imageUrl);
        Task ChangePasswordAsync(int userId, string newPassword);
    }
}
