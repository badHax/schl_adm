using AutoFixture.Xunit2;
using Moq;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using SchoolAdmin.Services;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace SchoolAdmin.Test
{
    public class CourseServiceTest
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Deregister Student that is not enrolled")]
        [AutoMoqData]
        public void Deregister_From_Course_Should_Throw_Not_Found_Ex_If_Enrollment_Is_null(CourseService sut, [Frozen]Mock<ICourseEnrollmentRepository> repo, string id, string cid)
        {
            //arrange
            repo.Setup(c => c.Get(id, cid)).Returns(new CourseEnrollment() { Id = null });

            //act
            var ex = Record.Exception(() => sut.DeregisterFromCourse(id, cid));

            //assert
            ex.ShouldNotBeNull();
            ex.GetBaseException().ShouldBeOfType<UpdateException>();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "No enrollment found for Id Given")]
        [AutoMoqData]
        public void Should_Throw_Not_Found_Ex_If_Application_User_Has_No_Enrollement([Frozen]Mock<ICourseEnrollmentRepository> repo, string id, string cid, CourseService sut)
        {
            //arrange
            repo.Setup(r => r.Get(id, cid)).Returns(new CourseEnrollment() { Id = null }); //empty

            //act
            var ex = Record.Exception(() => sut.GetEnrollmentDetails(id, cid));

            //assert
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType(typeof(NotFoundException));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Teacher not course admin")]
        [AutoMoqData]
        public void Should_Return_False_If_User_Not_Teacher_For_Course([Frozen]Mock<ICourseEnrollmentRepository> repo, string teacherId, string teacherId2, string courseId, CourseService sut)
        {
            //arrange
            repo.Setup(c => c.Get(teacherId, courseId)).Returns(new CourseEnrollment() { Id = "2", IsAdmin = true, CourseId = courseId });
            repo.Setup(c => c.Get(teacherId2, courseId)).Returns(new CourseEnrollment() { Id = null, IsAdmin = false, CourseId = null });

            //act
            var result = sut.IsTeacherForCourse(teacherId, courseId);
            var result2 = sut.IsTeacherForCourse(teacherId2, courseId);

            //assert
            result.ShouldBe(true);
            result2.ShouldBe(false);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "GetCourseDetails no course not found")]
        [AutoMoqData]
        public void Get_Course_Details_Should_Throw_If_Empty_Course_Object(CourseService sut, [Frozen]Mock<ICourseRepository> repo, string courseId)
        {
            //arrange
            repo.Setup(r => r.Get(courseId)).Returns(new Course() { Id = null });

            //act
            var ex = Record.Exception(() => sut.GetCourseDetails(courseId));

            //assert
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType(typeof(NotFoundException));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "User not registered")]
        [AutoMoqData]
        public void IsEnrolled_Returns_False_If_User_Not_Enrolled([Frozen]Mock<ICourseEnrollmentRepository> repo, string student1, string student2, string cid, CourseService sut)
        {
            //arrange
            repo.Setup(r => r.Get(student1, cid)).Returns(new CourseEnrollment() { Id = null });
            repo.Setup(r => r.Get(student2, cid)).Returns(new CourseEnrollment() { Id = "valid id" });

            //act
            bool result1 = sut.IsEnrolled(student1, cid);
            bool result2 = sut.IsEnrolled(student2, cid);

            //asssert
            result1.ShouldBe(false);
            result2.ShouldBe(true);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Register Duplicate Student")]
        [AutoMoqData]
        public void Register_Student_To_Course_Should_Not_Add_Duplicate([Frozen]Mock<IStudentRepository> srepo, [Frozen]Mock<ICourseEnrollmentRepository> cerepo, [Frozen]Mock<ICourseRepository> crepo, string student1, string student2, string course1, CourseService sut)
        {
            //arrange
            cerepo.Setup(r => r.Get(student1, course1)).Returns(new CourseEnrollment() { Id = "valid id" });      //student1 already enrolled
            cerepo.Setup(r => r.Get(student2, course1)).Returns(new CourseEnrollment() { Id = null });            //student2 not enrolled
            crepo.Setup(r => r.Get(course1)).Returns(new Course() { Id = "valid id" });                              //exist
            srepo.Setup(r => r.Get(student1)).Returns(new Student() { Id = "valid id" });                           //exist
            srepo.Setup(r => r.Get(student2)).Returns(new Student() { Id = null });                                 //exist

            //act
            var ex1 = Record.Exception(() => sut.RegisterStudentToCourse(student1, course1));
            var ex2 = Record.Exception(() => sut.RegisterStudentToCourse(student2, course1));

            //assert
            ex1.ShouldNotBeNull();
            ex1.GetBaseException().ShouldBeOfType<UpdateException>();
            ex2.ShouldBeNull();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Register Duplicate Teacher")]
        [AutoMoqData]
        public void Register_Teacher_To_Course_Should_Not_Add_Duplicate([Frozen]Mock<ICourseEnrollmentRepository> repo, [Frozen]Mock<ITeacherRepository> trepo, [Frozen]Mock<ICourseRepository> crepo, string teacher1, string teacher2, string course1, CourseService sut)
        {
            //arrange
            repo.Setup(r => r.Get(teacher1, course1)).Returns(new CourseEnrollment() { Id = "valid id" });      //teacher1 already exists
            repo.Setup(r => r.Get(teacher2, course1)).Returns(new CourseEnrollment() { Id = null });            //teacher2 does not exist
            crepo.Setup(r => r.Get(course1)).Returns(new Course() { Id = "valid id" });
            trepo.Setup(r => r.Get(teacher1)).Returns(new Teacher() { Id = "valid id" });                           //exist
            trepo.Setup(r => r.Get(teacher2)).Returns(new Teacher() { Id = null });

            //act
            var ex1 = Record.Exception(() => sut.RegisterTeacherToCourse(teacher1, course1));
            var ex2 = Record.Exception(() => sut.RegisterTeacherToCourse(teacher2, course1));

            //assert
            ex1.ShouldNotBeNull();
            ex1.ShouldBeOfType(typeof(UpdateException));
            ex2.ShouldBeNull();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Get course enrollment with no regustered student")]
        [AutoMoqData]
        public void GetRegisteredStudentsForCourse_Returns_Empty_Student_List_If_No_Student_Registered([Frozen]Mock<ICourseEnrollmentRepository> cerepo, [Frozen]Mock<ICourseRepository> crepo, string course1, string course2, CourseService sut)
        {
            //arrange
            crepo.Setup(c => c.Get(course1)).Returns(new Course() { Id = "valid id" });                         //course1 exist
            cerepo.Setup(c => c.GetAll()).Returns(new List<CourseEnrollment>());                                //no enrollment to course 1

            //act
            var ex = Record.Exception(() => sut.GetRegisteredStudentsForCourse(course1));
            var result = sut.GetRegisteredStudentsForCourse(course1);

            //assert
            ex.ShouldBeNull();

            result.Count.ShouldBe(0);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Get course enrollment for non existent course")]
        [AutoMoqData]
        public void GetRegisteredStudentsForCourse_Throws_For_Non_Existent_Course([Frozen]Mock<ICourseRepository> crepo, string courseId, CourseService sut)
        {
            //arrange
            crepo.Setup(c => c.Get(courseId)).Returns(new Course() { Id = null });

            //act
            var ex2 = Record.Exception(() => sut.GetRegisteredStudentsForCourse(courseId));

            //assert
            ex2.ShouldBeOfType<NotFoundException>();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
