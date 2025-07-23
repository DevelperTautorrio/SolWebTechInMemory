using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPLICATION.DTOs.Request;
using APPLICATION.DTOs.Response;
using APPLICATION.Interfaces;
using AutoMapper;
using DOMAIN.ENTITIES;

namespace APPLICATION.Services
{
    public class UserServices : IUserService
    {
        private readonly IMapper _mapper;
        private readonly B_User _businessLayer;

        public UserServices(IMapper mapper, B_User businessLayer)
        {
            _mapper = mapper;
            _businessLayer = businessLayer;
        }

        public async Task<UserResponseDto> GetUserByIdAsync(int id)
        {
            var user = await _businessLayer.GetById(id);
            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _businessLayer.GetAll();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<int> CreateUserAsync(UserCreateDto userDto)
        {
            var user = _mapper.Map<E_User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            return await _businessLayer.Create(user);
        }

        public async Task UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            var user = _mapper.Map<E_User>(userDto);
            user.UserID = id;
            await _businessLayer.Update(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _businessLayer.Delete(id);
        }

        public async Task<UserResponseDto> GetUserByEmailAsync(string email)
        {
            var user = await _businessLayer.GetByEmail(email);
            return _mapper.Map<UserResponseDto>(user);
        }

        public Task UpdateUserProfilePictureAsync(int userId, string imageUrl)
        {
            throw new NotImplementedException();
        }

        public Task ChangePasswordAsync(int userId, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
