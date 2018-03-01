using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolAdmin.Domain
{
    public class Course
    {
        [Key]
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string  CourseDescription { get; set; }
        public virtual Teacher Teacher { get; set; }
        public decimal CreditHours { get; set; }
        public string TeacherId { get; set; }
        // public virtual List<Grade> Grades { get; set; }
        //public virtual List<Student> Students { get; set; }
        

    }
}