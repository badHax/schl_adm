using Microsoft.AspNet.Identity.EntityFramework;
using SchoolAdmin.Domain;
using System.Data.Entity;

namespace SchoolAdmin.MVC.Models
{
    public class SchoolDbContext : IdentityDbContext<ApplicationUser>
    {
        public SchoolDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new Mappings.TeacherMap());
            //modelBuilder.Configurations.Add(new Mappings.StudentMap());
            //modelBuilder.Configurations.Add(new Mappings.CourseMap());
            //modelBuilder.Configurations.Add(new Mappings.GradeMap());
            base.OnModelCreating(modelBuilder);
        }

        public static SchoolDbContext Create()
        {
            return new SchoolDbContext();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                string msg = "";
                var enumerator = e.EntityValidationErrors.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    msg += enumerator.Current;
                }

                throw new System.Exception(msg);
            }
            catch (System.Exception) { throw; }
        }
    }
}