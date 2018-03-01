using SchoolAdmin.MVC.Controllers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace SchoolAdmin.Test
{
    public class TeacherServiceTest
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [AutoMoqData]
        public void Teacher_Role_Should_Be_Able_To_Edit_Grade(GradeController sut)
        {
            //arrange
            var roles = new string[] { "teacher" };
            var method = sut.GetType().GetMethod("Index");
            //act
            var attrib = method.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault() as AuthorizeAttribute;
            if (attrib == null)
            {
                throw new Exception();
            }
            //assert
            attrib.Roles.ShouldContain(roles[0]);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [AutoMoqData]
        public void Teacher_Role_Should_Be_Able_To_Add_Grade(GradeController sut)
        {
            //arrange
            var roles = new string[] { "teacher" };
            var method = sut.GetType().GetMethod("Index");

            //act
            var attrib = method.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault() as AuthorizeAttribute;
            if (attrib == null) { throw new Exception(); }

            //assert
            attrib.Roles.ShouldContain(roles[0]);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [AutoMoqData]
        public void Teacher_Role_Should_Be_Able_To_Update_Grade(GradeController sut)
        {
            //arrange
            var roles = new List<string> { "teacher" };
            var unauthorizedRoles = new List<string> { "student", "prospect" };
            var method = sut.GetType().GetMethod("AddGrade");
            //act
            var attrib = method.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault() as AuthorizeAttribute;
            if (attrib == null)
            {
                throw new Exception();
            }
            //assert
            attrib.Roles.ShouldContain(roles[0]);
            unauthorizedRoles.ForEach(r => attrib.Roles.ShouldNotContain(r));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //[Theory]
        //[AutoMoqData]
        //public void Teacher_Should_Access_Course_If_They_Are_Course_Admin([Frozen]Mock<IGradeService> gradeService, ApplicationService sut, Teacher teacher1, Teacher teacher2)
        //{
        //    //arrange
        //    gradeService.Setup(g => g.IsCourseAdministrator(teacher1.Id)).Returns(true);    // is course teacher
        //    gradeService.Setup(g => g.IsCourseAdministrator(teacher2.Id)).Returns(false);   // is NOT course teacher

        //    //act
        //    var authorizedAccess = Record.Exception(() => sut.GetStudentGrade(teacher1.Id, "", ""));
        //    Action unauthorizedAccess = () => sut.GetStudentGrade(teacher2.Id, "", "");

        //    //assert
        //    teacher1.ShouldNotBe(teacher2);
        //    authorizedAccess.ShouldNotBeOfType(typeof(AuthException));
        //    Should.Throw<AuthException>(unauthorizedAccess);
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [Theory]
        [AutoMoqData]
        public void User_Shuld_Have_Teacher_or_Admin_Role_To_Edit_Grades(GradeController sut)
        {
            //arrange
            var roles = new string[] { "teacher" };
            var method = sut.GetType().GetMethod("EditGrade");
            //act
            var attrib = method.GetCustomAttributes(typeof(AuthorizeAttribute), true).FirstOrDefault() as AuthorizeAttribute;
            if (attrib == null)
            {
                throw new Exception();
            }
            //assert
            attrib.Roles.ShouldContain(roles[0]);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
