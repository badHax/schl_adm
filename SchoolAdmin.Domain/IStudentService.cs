using System.Collections.Generic;

namespace SchoolAdmin.Domain
{
    public interface IStudentService
    {
        Student GetStudentDetails(string id);
        decimal GetStudentGPA(Student student);
        List<Course> GetAllEnrolledCourses(string studentId);
        List<Student> GetAllStudents();
        bool AddNew(Student student);
        bool TransferToGrade(string studentId, string classId);
        void DeregisterStudent(string studentId);
        List<Course> GetAllStudentCourses(string studentId);
    }
}
