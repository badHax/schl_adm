using System.Data.Entity;
using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.DbContext
{
    public interface ISchoolDbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<Grade> Grades { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<CourseEnrollment> Enrollment { get; set; }

        int SaveChanges();
    }
}