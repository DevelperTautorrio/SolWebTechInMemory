using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Response;
using APPLICATION.Interfaces;

namespace APPLICATION.Services
{
    public class ProfileServices : IUserProfileService
    {
        private readonly B_User _businessLayer;

        public ProfileServices(B_User businessLayer)
        {
            _businessLayer = businessLayer;
        }

        public async Task UpdateProfilePictureAsync(int userId, string imageUrl)
        {
            var user = await _businessLayer.GetById(userId);
            user.ProfilePicture = imageUrl;
            await _businessLayer.Update(user);
        }

        public async Task UpdateBiographyAsync(int userId, string biography)
        {
            var user = await _businessLayer.GetById(userId);
            user.Biography = biography;
            await _businessLayer.Update(user);
        }

        public Task<IEnumerable<UserResponseDto>> SearchUsersAsync(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
