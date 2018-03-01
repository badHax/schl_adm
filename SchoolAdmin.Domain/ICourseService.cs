using System.Collections.Generic;

namespace SchoolAdmin.Domain
{
    public interface ICourseService
    {
        bool IsTeacherForCourse(string teacherId, string courseId);
        bool IsEnrolled(string id, string courseId);
        void RegisterStudentToCourse(string studentId, string courseId);
        void DeregisterFromCourse(string studentId, string courseId);
        List<Student> GetRegisteredStudentsForCourse(string courseId);
        List<Course> GetAllCourses();
        Course GetCourseDetails(string courseId);
        CourseEnrollment GetEnrollmentDetails(string Id, string courseId);
    }
}
