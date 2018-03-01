using SchoolAdmin.Domain;
using SchoolAdmin.Util.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SchoolAdmin.Services
{

    /*
     * class that calculates a student’s GPA on a 4.2 scale. 
     * Grade point average (GPA) is calculated by dividing the total amount
     * of grade points earned by the total amount of credit hours attempted.
     * 
     * For each hour, an A receives 4 grade points, a B receives 3 grade points,
     * a C receives 2 grade points, and a D receives 1 grade point.
     */
    public class GPACalculator42Scale : IGPACalculator1
    {
        private Dictionary<string, decimal> _letterGrades;
        private List<Grade> _grades;

        public GPACalculator42Scale()
        {
            this._letterGrades = new Dictionary<string, decimal>();

            _letterGrades["APLUS"] = 95;
            _letterGrades["A"] = 90;
            _letterGrades["AMINUS"] = 85;
            _letterGrades["BPLUS"] = 80;
            _letterGrades["B"] = 75;
            _letterGrades["BMINUS"] = 70;
            _letterGrades["CPLUS"] = 65;
            _letterGrades["C"] = 60;
            _letterGrades["CMINUS"] = 55;
            _letterGrades["DPLUS"] = 50;
            _letterGrades["D"] = 45;
            _letterGrades["DMINUS"] = 40;
            _letterGrades["F"] = 0;
        }

        public decimal CalculateGPA(List<Grade> grades)
        {
            this._grades = grades;
            if (_grades == null) { throw new System.Exception("No grades given."); }
            if ( _grades.Count == 0) { return 0; }

            //credit hours attempted
            decimal attempted = GetCreditHoursAttempted();

            //credit hours passed
            decimal earned = GetCreditHoursEarned();

            //gpa
            return decimal.Round( earned / attempted, 2, System.MidpointRounding.AwayFromZero);

        }

        private decimal GetGradePoint(int gradeValue)
        {
            string grade = "";
            foreach (var letter in _letterGrades.OrderByDescending(letter => letter.Value))
            {
                if ( gradeValue >= letter.Value)
                {
                    grade = letter.Key;
                    break;
                }
            }

            switch (grade)
            {
                case "APLUS": { return 4.20m; }
                case "A": { return 4.00m ; }
                case "AMINUS": { return 3.66m; }
                case "BPLUS": { return 3.33m; }
                case "B": { return 3.00m; }
                case "BMINUS": { return 2.66m; }
                case "CPLUS": { return 2.33m; }
                case "C": { return 2.00m; }
                case "CMINUS": { return 1.66m; }
                case "DPLUS": { return 1.33m; }
                case "D": { return 1.00m; }
                case "DMINUS": { return 0.66m; }
                default: { return 0.00m; }
            }
        }

        private decimal GetCreditHoursAttempted()
        {
            decimal hours = 0;
            _grades.ForEach(g => hours += g.Course.CreditHours);
            return hours;
        }

        private decimal GetCreditHoursEarned()
        {
            decimal hours = 0;
            _grades.ForEach(g => hours += (GetGradePoint(g.Value)) * g.Course.CreditHours);
            return hours;
        }

        decimal IGPACalculator1.CalculateTermGPA()
        {
            throw new System.NotImplementedException();
        }
    }
}
