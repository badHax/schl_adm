using Microsoft.AspNet.Identity.EntityFramework;
using SchoolAdmin.DataAccess.ViewModels;
using SchoolAdmin.Domain;
using System.Data.Entity;

namespace SchoolAdmin.DataAccess.DbContext
{
    public class SchoolDbContext : IdentityDbContext<ApplicationUser>, ISchoolDbContext
    {
        public SchoolDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<CourseEnrollment> Enrollment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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