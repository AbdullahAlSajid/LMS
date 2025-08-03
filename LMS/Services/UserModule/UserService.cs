using LMS.Data;
using LMS.Models.Dtos;
using LMS.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services.UserModule
{
    public class UserService : IUserService
    {
        private readonly LMSDbContext _context;

        public UserService(LMSDbContext context) 
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Where(u => u.DeletedAt == null).ToListAsync();

            return users;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null || user.DeletedAt != null)
                return null;

            return user;
        }

        public async Task<User> CreateUser(UserDto userDto)
        {
            var user = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = userDto.PasswordHash
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(int id, UserDto updatedUser)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null || user.DeletedAt != null)
                return null;

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.PasswordHash = updatedUser.PasswordHash;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null || user.DeletedAt != null)
                return false;

            user.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
