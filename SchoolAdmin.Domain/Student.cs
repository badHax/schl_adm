using System.Collections.Generic;

namespace SchoolAdmin.Domain
{
    public class Student : ApplicationUser
    {
        public virtual List<Grade> Grades { get; set; }
    }
}