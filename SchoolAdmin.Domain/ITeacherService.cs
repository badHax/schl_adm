using System.Collections.Generic;

namespace SchoolAdmin.Domain
{
    public interface ITeacherService
    {
        List<Course> ListTeacherCourses(string teacherId);
    }
}
