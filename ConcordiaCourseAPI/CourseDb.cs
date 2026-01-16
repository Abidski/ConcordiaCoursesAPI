using Microsoft.EntityFrameworkCore;
namespace ConcordiaCourseAPI;

public class CourseDb:DbContext
{
    public CourseDb(DbContextOptions<CourseDb> options):base(options)
    {
        
    }

    public DbSet<Course> Courses => Set<Course>();
}