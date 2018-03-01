using System.Collections.Generic;
using SchoolAdmin.Domain;

namespace SchoolAdmin.DataAccess.Interfaces
{
    public interface ICourseEnrollmentRepository
    {
        string Add(CourseEnrollment enrollment);
        void AddAsync(CourseEnrollment enrollments);
        CourseEnrollment Get(string id, string courseId);
        List<CourseEnrollment> GetAll();
        void Remove(CourseEnrollment enrollment);
        void Update(CourseEnrollment enrollment);
    }
}