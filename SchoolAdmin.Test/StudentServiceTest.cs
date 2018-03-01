using AutoFixture.Xunit2;
using Moq;
using SchoolAdmin.DataAccess.Interfaces;
using SchoolAdmin.Domain;
using SchoolAdmin.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace SchoolAdmin.Test
{
    public class StudentServiceTest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName ="Deregister student should withdraw student from current courses")]
        [AutoMoqData]
        public void DeregisterStudent_Should_Remove_All_Enrollment([Frozen]Mock<ICourseEnrollmentRepository> cerepo,[Frozen]Mock<IStudentRepository> srepo, Student student, string studentId, StudentService sut)
        {
            //arrange
            var e1 = new CourseEnrollment() { Id = student.Id, CourseId = "x" };
            var e2 = new CourseEnrollment() { Id = student.Id, CourseId = "y" };
            var enrollment = new List<CourseEnrollment>() { e1, e2 };

            srepo.Setup(r => r.Get(studentId)).Returns(new Student());
            cerepo.Setup(r => r.GetAll()).Returns(enrollment);
            cerepo.Setup(r => r.Remove(e1)).Callback(() => enrollment.Remove(e1));
            cerepo.Setup(r => r.Remove(e2)).Callback(() => enrollment.Remove(e2));

            //act
            sut.DeregisterStudent(student.Id);

            //assert
            enrollment.ShouldBeEmpty();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Get student details does not return result")]
        [AutoMoqData]
        public void GetStudentDetails_Throws_Not_Found_Exception_For_Empty_Result([Frozen]Mock<IStudentRepository> srepo, string studentId, StudentService  sut)
        {
            //arrange
            srepo.Setup(r => r.Get(studentId)).Returns(new Student() { Id = null });

            //act
            var ex = Record.Exception(() => sut.GetStudentDetails(studentId));

            //assert
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<NotFoundException>();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Get GPA of not enrolled student")]
        [AutoMoqData]
        public void GetStudentGPATest_Throws_Exception_If_Student_Not_Registered()
        {
            throw new NotImplementedException();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact()]
        public void All_MethodTest()
        {
            throw new NotImplementedException();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact()]
        public void AddNew_Enrolls_Duplicate_Student_Test()
        {
            throw new NotImplementedException();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Get all courses student is registered for")]
        [AutoMoqData]
        public void GetAllStudentsCourse_Should_Return_All_Courses_Student_Registered_To([Frozen]Mock<ICourseEnrollmentRepository> cerepo, [Frozen]Mock<IStudentRepository> srepo, CourseEnrollment e1, CourseEnrollment e2, string studentId, StudentService sut)
        {
            //arrange
            srepo.Setup(s => s.Get(studentId)).Returns(new Student() { Id =  "valid id"});
            cerepo.Setup(c => c.GetAll()).Returns(new List<CourseEnrollment>() { e1, e2 });

            //act
            var result = sut.GetAllStudentCourses(studentId);

            //assert
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Fact()]
        public void TransferToGradeTest()
        {
            throw new NotImplementedException();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "Deregister non-existent student")]
        [AutoMoqData]
        public void DeregisterStudent_Should_Throw_If_Student_Does_Not_Exist([Frozen]Mock<IStudentRepository> srepo, [Frozen]Student student, string studentId, StudentService sut)
        {
            //arrange
            srepo.Setup(r => r.Get(studentId)).Returns(new Student() { Id = null });

            //act
            var ex = Record.Exception(() => sut.DeregisterStudent(studentId));

            //assert
            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<UpdateException>();
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

       

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}