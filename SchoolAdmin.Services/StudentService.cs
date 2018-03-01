using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using SchoolAdmin.Util.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SchoolAdmin.Services
{
    public class StudentService : IStudentService
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private readonly IStudentRepository _srepo;
        private readonly IGPACalculator1 _calculator;
        private readonly ICourseEnrollmentRepository _cerepo;
        private readonly ICourseRepository _crepo;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public StudentService(IStudentRepository srepo, IGPACalculator1 calculator ,ICourseEnrollmentRepository cerepo, ICourseRepository crepo)
        {
            this._srepo = srepo;
            this._calculator = calculator;
            this._cerepo = cerepo;
            this._crepo = crepo;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Student GetStudentDetails(string id)
        {
            var student = _srepo.Get(id) ?? new Student();
            if(student.Id == null)
            {
                throw new NotFoundException(string.Format("Error retrieving details for student with id {0}",id));
            }
            return student;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public decimal GetStudentGPA(Student student)
        {
            var gpa = _calculator.CalculateGPA(student.Grades);

            return gpa;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Student> All()
        {
            return _srepo.GetAll();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public bool AddNew(Student student)
        {
            if (_srepo.Add(student) == "duplicate")
            {
                return false;
            }
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Student> GetAllStudents()
        {
            return _srepo.GetAll() ?? new List<Student>();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool TransferToGrade(string studentId, string classId)
        {
            throw new System.NotImplementedException();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void DeregisterStudent(string studentId)
        {
            var student = _srepo.Get(studentId) ?? new Student();
            //is this a valid student?
            if (student.Id == null)
            {
                throw new UpdateException(string.Format("student with id {0}",studentId));
            }
            //deregister the student from all courses they were registered to
            //here
            var enrollment = _cerepo.GetAll();
            foreach (var item in enrollment)
            {
                if (item.Id ==studentId)
                {
                    _cerepo.Remove(item);
                }
            }
            var result = _srepo.Remove(student);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Course> GetAllStudentCourses(string studentId)
        {
            List<Course> courses = new List<Course>();
            var student = _srepo.Get(studentId) ?? new Student() { Id = null};
            if(student.Id == null)
            {
                throw new NotFoundException(string.Format("Unable to get courses for student with Id {0}", studentId));
            }
            var enrollment = _cerepo.GetAll().Where(e => e.Id == studentId) ?? new List<CourseEnrollment>();
            enrollment.ToList().ForEach( x => courses.Add(_crepo.Get(x.CourseId)));
            return courses;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<Course> GetAllEnrolledCourses(string studentId)
        {
            try
            {
                var student = GetStudentDetails(studentId);
                var enrollment = _cerepo.GetAll().Where(e => e.Id == studentId).ToList();

                var courses = new List<Course>();
                enrollment.ForEach(e => courses.Add(_crepo.Get(e.CourseId)));

                return courses;
            }
            catch (NotFoundException)
            {
                throw;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
