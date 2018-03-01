using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin.Domain
{

    /*
     * class that calculates a student’s GPA on a 4.0 scale. 
     * Grade point average (GPA) is calculated by dividing the total amount
     * of grade points earned by the total amount of credit hours attempted.
     * 
     * For each hour, an A receives 4 grade points, a B receives 3 grade points,
     * a C receives 2 grade points, and a D receives 1 grade point.
     */
    public class CumulativeGPACalculator
    {
        private List<Grade> _grades;
        private Dictionary<string, int> _letterGrades;
       

        public CumulativeGPACalculator(List<Grade> grade)
        {
            this._grades = grade;
            this._letterGrades = new Dictionary<string, int>();

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

        public double CalculateGPA()
        {
            if( _grades.Count == 0) { return 0; }

            //credit hours attempted
            double attempted = GetCreditHoursAttempted();

            //credit hours passed
            double earned = GetCreditHoursEarned();

            //gpa
            return earned / attempted;

        }

        private double GetGradePoint(int gradeValue)
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
                case "APLUS": { return 4.2; }
                case "A": { return 4.00 ; }
                case "AMINUS": { return 3.66; }
                case "BPLUS": { return 3.33; }
                case "B": { return 3.00; }
                case "BMINUS": { return 2.66; }
                case "CPLUS": { return 2.33; }
                case "C": { return 2; }
                case "CMINUS": { return 1.66; }
                case "DPLUS": { return 1.33; }
                case "D": { return 1.00; }
                case "DMINUS": { return 0.66; }
                default: { return 0.00; }
            }
        }

        private double GetCreditHoursAttempted()
        {
            double hours = 0;
            _grades.ForEach(g => hours += g.Course.CreditHours);
            return hours;
        }

        private double GetCreditHoursEarned()
        {
            double hours = 0;
            _grades.ForEach(g => hours += (GetGradePoint(g.Value)) * g.Course.CreditHours);
            return hours;
        }

    }
}
