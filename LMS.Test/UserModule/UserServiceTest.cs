using LMS.Data;
using LMS.Services.UserModule;
using Microsoft.EntityFrameworkCore;

namespace LMS.Test.UserModule
{
    public class UserServiceTest
    {
        private readonly LMSDbContext _context;
        private readonly UserService _service;

        public UserServiceTest ()
        {
            var options = new DbContextOptionsBuilder<LMSDbContext>().Options;

            _context = new LMSDbContext(options);
            _service = new UserService(_context);
        }


    }
}
