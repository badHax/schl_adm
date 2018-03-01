using SchoolAdmin.Domain;
using System.Collections.Generic;

namespace SchoolAdmin.Util.Interfaces
{
    public interface IGPACalculator1
    {
        decimal CalculateGPA(List<Grade> grades);
        decimal CalculateTermGPA();
    }
}