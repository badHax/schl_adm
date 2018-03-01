using System.Collections.Generic;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using System.Linq;

namespace SchoolAdmin.Services
{
    public class CourseService : ICourseService
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly ICourseEnrollmentRepository _cerepo;
        private readonly ICourseRepository _crepo;
        private readonly IStudentRepository _srepo;
        private readonly ITeacherRepository _trepo;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public CourseService(ICourseEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository)
        {
            _cerepo = enrollmentRepository;
            _crepo = courseRepository;
            _srepo = studentRepository;
            _trepo = teacherRepository;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void DeregisterFromCourse(string Id, string courseId)
        {
            CourseEnrollment enrollment = _cerepo.Get(Id, courseId) ?? new CourseEnrollment();
            if (enrollment.Id == null)
            {
                throw new UpdateException(string.Format("No enrollment record found for user with id {0}", Id));
            }
            _cerepo.Remove(enrollment);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Course GetCourseDetails(string Id)
        {
            Course course = _crepo.Get(Id) ?? new Course();
            if (course.Id == null) { throw new NotFoundException(string.Format("No course record found for user with id {0}", Id)); }
            return course;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public CourseEnrollment GetEnrollmentDetails(string Id, string courseId)
        {
            CourseEnrollment enrollment = _cerepo.Get(Id,courseId) ?? new CourseEnrollment();
            if (enrollment.Id == null) { throw new NotFoundException(string.Format("No enrollment record found for user with id {0}", Id)); }
            return enrollment;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsEnrolled(string studentId, string courseId)
        {
            CourseEnrollment enrollment = _cerepo.Get(studentId, courseId) ?? new CourseEnrollment();
            if (enrollment.Id == null) { return false; }
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool IsTeacherForCourse(string teacherId, string courseId)
        {
            try
            {
                var enrollment = GetEnrollmentDetails(teacherId, courseId);
                return enrollment.IsAdmin;
            }
            catch (NotFoundException)
            {
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void RegisterStudentToCourse(string studentId, string courseId)
        {
            var student = _srepo.Get(studentId) ?? new Student();
            var course = _crepo.Get(courseId) ?? new Course();
            if (student.Id != null)
            {
                throw new UpdateException(string.Format("Student : {0} already exist",studentId) );
            }
            if(course.Id == null)
            {
                throw new UpdateException(string.Format("Course : {0} not found", courseId));
            }
            var enrollment = _cerepo.Get(studentId,courseId) ?? new CourseEnrollment();
            if (enrollment.Id != null)
            {
                throw new UpdateException(string.Format("Student {0} already registered for Course : {1} already found", student,courseId));
            }
            CourseEnrollment e = new CourseEnrollment() { Id = studentId, CourseId = courseId, IsAdmin = false };
            _cerepo.AddAsync(e);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void RegisterTeacherToCourse(string teacherId, string courseId)
        {
            var teacher = _trepo.Get(teacherId) ?? new Teacher();
            var course = _crepo.Get(courseId) ?? new Course();
            if (teacher.Id != null)
            {
                throw new UpdateException(string.Format("Teacher : {0} already exist", teacherId));
            }
            if (course.Id == null)
            {
                throw new UpdateException(string.Format("Course : {0} not found", courseId));
            }
            var enrollment = _cerepo.Get(teacherId, courseId) ?? new CourseEnrollment();
            if (enrollment.Id != null)
            {
                throw new UpdateException(string.Format("Teacher {0} already registered for Course : {1} already found", teacher, courseId));
            }
            CourseEnrollment e = new CourseEnrollment() { Id = teacherId, CourseId = courseId, IsAdmin = true };
            _cerepo.AddAsync(e);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Student> GetRegisteredStudentsForCourse(string courseId)
        {
            var students = new List<Student>();
            var course = _crepo.Get(courseId) ?? new Course();
            if(course.Id == null)
            {
                throw new NotFoundException("msg");
            }
            var enrolled = _cerepo.GetAll().Where(x => x.CourseId == course.Id && x.IsAdmin == false ).ToList() ?? new List<CourseEnrollment>();
            if (enrolled.Count() == 0)
            {
                return new List<Student>();
            }
            enrolled.ForEach(e => students.Add(_srepo.Get(e.Id)));
            return students;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Course> GetAllCourses()
        {
            var result = _crepo.GetAll() ?? new List<Course>();
            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
