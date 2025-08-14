using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolGrading
{
    // Custom exception
    public class InvalidGradeException : Exception
    {
        public InvalidGradeException(string message) : base(message) { }
    }

    // Student class
    public class Student
    {
        public string Name { get; }
        public double Grade { get; }

        public Student(string name, double grade)
        {
            if (grade < 0 || grade > 100)
                throw new InvalidGradeException($"Invalid grade for {name}: {grade}");
            Name = name;
            Grade = grade;
        }
    }

    // Grade calculator
    public class GradeCalculator
    {
        public static string GetLetterGrade(double grade)
        {
            if (grade >= 80) return "A";
            if (grade >= 70) return "B";
            if (grade >= 60) return "C";
            if (grade >= 50) return "D";
            return "F";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("SCHOOL GRADING SYSTEM (Q4)\n");

            string inputFile = "grades.txt";
            string outputFile = "results.txt";

            // Create sample grades file
            File.WriteAllLines(inputFile, new string[]
            {
                "Ibrahim,85",
                "Ama,72",
                "Kwame,45",
                "Akosua,110" // invalid
            });

            var students = new List<Student>();

            try
            {
                foreach (var line in File.ReadAllLines(inputFile))
                {
                    var parts = line.Split(',');
                    string name = parts[0];
                    double grade = double.Parse(parts[1]);

                    var student = new Student(name, grade);
                    students.Add(student);
                }
            }
            catch (InvalidGradeException ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Unexpected: {ex.Message}");
            }

            // Write results
            using (var writer = new StreamWriter(outputFile))
            {
                foreach (var s in students)
                {
                    string letter = GradeCalculator.GetLetterGrade(s.Grade);
                    writer.WriteLine($"{s.Name}: {s.Grade} => {letter}");
                }
            }

            Console.WriteLine($"Grading complete. Results written to '{outputFile}'.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
