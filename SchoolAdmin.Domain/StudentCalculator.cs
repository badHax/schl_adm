using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolAdmin.Domain
{
    public class StudentCalculator
    {
        private List<Grade> grades;

        public StudentCalculator()
        {
            this.grades = grades;
        }

        public int StudentAverage(List<Grade> grades)
        {
            int average = 0;
            grades.ForEach(g => average += g.Value);

            return average / grades.Count();
        }

        public int StudentGPA(Student student)
        {
            return student.CreditsEarned / student.CreditsAttempted;
        }
    }
}
