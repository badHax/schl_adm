//////////////////////////////////////////////////

using System.Collections.Generic;
using SchoolAdmin.Domain;
using SchoolAdmin.DataAccess.Interfaces;
using System.Linq;

//////////////////////////////////////////////////

namespace SchoolAdmin.Services
{ 
    public class TeacherService : ITeacherService
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly ITeacherRepository _trepo;
        private readonly ICourseRepository _crepo;
        private readonly ICourseEnrollmentRepository _cerepo;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public TeacherService(ITeacherRepository teacherRepository, ICourseEnrollmentRepository courseEnrollmentRepository, ICourseRepository courseRepository)
        {
            _trepo = teacherRepository;
            _crepo = courseRepository;
            _cerepo = courseEnrollmentRepository;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Course> ListTeacherCourses(string teacherId)
        {
            var courses = new List<Course>();
            var teacher = _trepo.Get(teacherId) ?? new Teacher();

            if (teacher.Id == null)
            {
                throw new NotFoundException(string.Format("cannot list courses. teacher with id {0} not found.",teacherId));
            }

            var enrollment = _cerepo.GetAll().Where(c => c.Id == teacherId && c.IsAdmin == true ) as List<CourseEnrollment>;
            if (enrollment.Count == 0)
            {
                return courses;
            }

            enrollment.ForEach(x => courses.Add(_crepo.Get(x.CourseId)));
            return courses;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
