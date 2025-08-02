using LMS.Models;

namespace LMS.Services
{
    public interface ICourseService
    {
        Task<Course> GetCourseByIdAsync(int id);
    }
}
