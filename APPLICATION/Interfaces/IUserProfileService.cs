using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Response;

namespace APPLICATION.Interfaces
{
    public interface IUserProfileService
    {
        Task UpdateBiographyAsync(int userId, string biography);
        Task<IEnumerable<UserResponseDto>> SearchUsersAsync(string keyword);
    }
}
