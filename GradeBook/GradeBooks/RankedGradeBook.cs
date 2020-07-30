using System;
using System.ComponentModel.Design;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students.");
            }

            // Count all Students passed in, and devide by 5 to get 20%
            // this becomes the threshold for dropping a grade rank
            // Example: 5 Students / 5 = 1, so every 1 Student you drop the grade
            var threshold = (int)Math.Ceiling(Students.Count * 0.2);

            // Sorts the Students by Average Grade, then Selects just the Average Grade
            // as we do not need the other information then put it into a list
            var grades = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList();

            // To get the Student from the grades list we need to do the Threshold - 1 because Lists use 0 based indexing
            // Then we check that the grade is less than or equal to the average grades
            if (grades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }
            else if (grades[(threshold * 2) - 1] <= averageGrade)
            {
                return 'B';
            }
            else if (grades[(threshold * 3) - 1] <= averageGrade)
            {
                return 'C';
            }
            else if (grades[(threshold * 4) - 1] <= averageGrade)
            {
                return 'D';
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }
    }
}
