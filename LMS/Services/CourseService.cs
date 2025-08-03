using LMS.Data;
using LMS.Models.Entities;
using System;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {
        private readonly LMSDbContext _context;
        public CourseService(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
    }
}
