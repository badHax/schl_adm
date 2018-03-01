

using System.Data.Entity.ModelConfiguration;

namespace SchoolAdmin.Domain
{
    public static class Mappings
    {
        internal class TeacherMap : EntityTypeConfiguration<Teacher>
        {
            public TeacherMap()
            {
                //primary key
                this.HasKey(t => t.Id);

                //set table name
                this.ToTable("teacher");

            }
        }

        internal class GradeMap : EntityTypeConfiguration<Grade>
        {
            public GradeMap()
            {
                //primary key
                this.HasKey(g => g.StudentId);

                //set table name
                this.ToTable("grade");

            }
        }

        internal class StudentMap : EntityTypeConfiguration<Student>
        {
            public StudentMap()
            {
                //primary key
                this.HasKey(t => t.Id);

                //set table name
                this.ToTable("student");

            }
        }

        internal class CourseMap : EntityTypeConfiguration<Course>
        {
            public CourseMap()
            {
                //primary key
                this.HasKey(c => c.Id);

                //set table name
                this.ToTable("course");

                //relationships
                this.HasRequired(c => c.Teacher)
                    .WithMany(t => t.Courses)
                    .HasForeignKey(c => c.TeacherId);
            }
        }
    }
}
