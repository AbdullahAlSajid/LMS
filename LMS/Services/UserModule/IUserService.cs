using LMS.Models.Dtos;
using LMS.Models.Entities;

namespace LMS.Services.UserModule
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<bool> DeleteUser(int id);
        public Task<User> UpdateUser(int id, UserDto user);
        public Task<User> GetUserById(int id);
        public Task<User> CreateUser(UserDto user);
    }
}
