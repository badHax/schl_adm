

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAdmin.Domain
{
    public class CourseEnrollment
    {
        [Key , Column(Order = 1)]
        public string Id { get; set; }
        [Key, Column(Order = 2)]
        public string CourseId { get; set; }
        public virtual List<ApplicationUser> students { get; set; }
        public virtual List<Course> courses { get; set; }
        public bool IsAdmin { get; set; }
    }
}
