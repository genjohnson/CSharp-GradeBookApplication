using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count() < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students.");
            }

            // Get an ordered list of average grades.
            var averageGrades = new List<double>();
            foreach (var student in Students)
            {
                averageGrades.Add(student.AverageGrade);
            }
            averageGrades.OrderByDescending(x => x); 

            // Determine which quintile averageGrade falls into based on the number of students.
            var averageGradeRank = averageGrades.IndexOf(averageGrade) + 1; // Add 1 to account for zero indexing.
            var rank = averageGradeRank / Students.Count();
            double twentyPercent = (double)Students.Count() / 5;
            twentyPercent = Math.Round(twentyPercent, 0);
            var gradeGroup = Math.Ceiling(averageGradeRank / twentyPercent);

            // Assign letter grade based on quintile.
            switch (gradeGroup)
            {
                case 1:
                    return 'A';
                case 2:
                    return 'B';
                case 3:
                    return 'C';
                case 4:
                    return 'D';
                default:
                    return 'F';
            }
        }

        public override void CalculateStatistics()
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
