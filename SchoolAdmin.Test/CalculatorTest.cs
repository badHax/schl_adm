/////////////////////////////////////

using SchoolAdmin.Domain;
using SchoolAdmin.Services;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

/////////////////////////////////////

namespace SchoolAdmin.Test
{
    public class CalculatorTest
    {

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName = "GPA should be 0 if no class taken")]
        [AutoMoqData]
        public void Should_return_Zero_If_No_Class_Taken(GPACalculator42Scale sut)
        {
            //arrange
            var expected = 0;
            var grades = new List<Grade>(); //empty

            //act
            var result = sut.CalculateGPA(grades);

            //assert
            result.ShouldBe(expected);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [Theory(DisplayName ="Throws If Grade has no course associated with it")]
        [AutoMoqData]
        public void Should_Throw_If_Grade_Has_No_Course_Item(GPACalculator42Scale sut)
        {
            //grades has no course object
            var grades = new List<Grade>()
            {
                new Grade(){Course = null },
                 new Grade(){Course = null },
                  new Grade(){Course = null }
            };

            Should.Throw<Exception>(() => sut.CalculateGPA(grades));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [Fact(DisplayName = "Test case: Calculate GPA given 3 specific grades")]
        public void Should_Return_Correct_GPA()
        {
            var sut = new GPACalculator42Scale();
            var grades = new List<Grade>()
            {
                new Grade(){ Course=new Course { CreditHours = 3 }, Value = 78},
                new Grade(){ Course=new Course { CreditHours = 6 }, Value = 96},
                new Grade(){ Course=new Course { CreditHours = 3 }, Value = 88}
            };

            sut.CalculateGPA(grades).ShouldBe(3.77m);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
