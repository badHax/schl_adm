using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdmin.Domain
{
    public class Grade
    {
        [Key, Column(Order = 1)]
        public string StudentId { get; set; }
        [Key, Column(Order = 2)]
        public string CourseId { get; set; }
        public virtual Course Course{ get; set; }
        public virtual ApplicationUser Student { get; set; }
        public int Value { get; set; }
        public int CreditHoursEarned { get; set; }
        public int Semester { get; set; }
    }
}
