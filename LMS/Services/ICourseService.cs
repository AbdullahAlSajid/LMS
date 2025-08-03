using LMS.Models.Entities;

namespace LMS.Services
{
    public interface ICourseService
    {
        Task<Course> GetCourseByIdAsync(int id);
    }
}
