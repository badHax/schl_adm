namespace SchoolAdmin.Frontend.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SchoolAdmin.DataAccess.EntityModels;
    using SchoolAdmin.Domain;
    using SchoolAdmin.Frontend.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolAdmin.DataAccess.EntityModels.SchoolDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SchoolAdmin.Frontend.Models.ApplicationDbContext";
        }

        protected override void Seed(SchoolAdmin.DataAccess.EntityModels.SchoolDbContext context)
        {
        #region admin
            //get managers
            UserManager<ApplicationUser> applicationUserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //create roles
            List<string> roles = new List<string> { "admin", "student", "teacher" };
            roles.ForEach(r => roleManager.Create(new IdentityRole(r)));
            context.SaveChanges();

            //create admin
            PasswordHasher passwordHasher = new PasswordHasher();
            var password = passwordHasher.HashPassword("P@ssw0rd");
            ApplicationUser user = new ApplicationUser() {Id = "112334" ,Email = "admin@admin.com", PasswordHash = password, UserName = "siteadmin"};
            var found = context.Users.Find(user.Id);
            if (found == null)
            {
                var result = applicationUserManager.Create(user);
                if (!result.Succeeded)
                {

                    var errors = result.Errors;
                    string msg = "";
                    var enumerate = result.Errors.GetEnumerator();
                    while (enumerate.MoveNext())
                    {
                        msg += enumerate.Current;
                    }
                    throw (new System.Exception(msg));
                }
            }

            //add to role
            applicationUserManager.AddToRole(user.Id,"admin");
            context.SaveChanges();
            #endregion
            #region dummy data
            var student1 = new Student() { Id = "1", FirstName = "Jim", LastName = "James" };
            var student2 = new Student() { Id = "2", FirstName = "Pat", LastName = "Palmer" };
            var student3 = new Student() { Id = "3", FirstName = "Carl", LastName = "Conner" };

            var course1 = new Course() { Id = "a", CourseName = "Pyhsics", CourseDescription = "Studies the physical properties of the universe", TeacherId = "t1", CreditHours = 3 };
            var course2 = new Course() { Id = "b", CourseName = "Chemistry", CourseDescription = "Studies the chemical properties of the universe", TeacherId = "t2", CreditHours = 6 };
            var course3 = new Course() { Id = "c", CourseName = "Biology", CourseDescription = "Studies livig organisms", TeacherId = "t3" , CreditHours = 3};
            var course4 = new Course() { Id = "d", CourseName = "Mathematics", CourseDescription = "Describes methods to quantify and model processes and objects", TeacherId = "t1" };

            var teacher1 = new Teacher() { Id = "t1", FirstName = "Rose", LastName = "Donavan" };
            var teacher2 = new Teacher() { Id = "t2", FirstName = "Chirs", LastName = "Lanal" };
            var teacher3 = new Teacher() { Id = "t3", FirstName = "O'Neil", LastName = "Bryan" };

            var grade1 = new Grade() { CourseId = "a", StudentId = "1", Value = 89 };
            var grade2 = new Grade() { CourseId = "b", StudentId = "1", Value = 90 };
            var grade3 = new Grade() { CourseId = "d", StudentId = "1", Value = 70 };
            var grade4 = new Grade() { CourseId = "a", StudentId = "2", Value = 60 };
            var grade5 = new Grade() { CourseId = "b", StudentId = "2", Value = 50 };
            var grade6 = new Grade() { CourseId = "a", StudentId = "3", Value = 89 };
            var grade7 = new Grade() { CourseId = "b", StudentId = "3", Value = 73 };
            var grade8 = new Grade() { CourseId = "c", StudentId = "3", Value = 89 };

            var teachers = new List<Teacher>() { teacher1, teacher2, teacher3 };
            var courses = new List<Course>() { course1, course2, course3, course4 };
            var student = new List<Student> { student1, student2, student3 };
            var grades = new List<Grade>() { grade1, grade2, grade3, grade4, grade5, grade6, grade7, grade8 };

            teachers.ForEach(t => context.Teachers.AddOrUpdate(t));
            courses.ForEach(c => context.Courses.AddOrUpdate(c));
            student.ForEach(s => context.Students.AddOrUpdate(s));
            grades.ForEach(g => context.Grades.AddOrUpdate(g));
            context.SaveChanges();
            #endregion
        }
    }
}
