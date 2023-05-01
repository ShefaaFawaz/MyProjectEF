using BetterConsoleTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EF.Management
{
    internal class StudentMarkManagement
    {
        static readonly ProjectDbContext _db = new();
        public static void Management()
        {
            while (true)
            {
                Table table = new("ID", "Exam ID", "Exam's Subject Name", "Student Id"
                    ,"Student First Name","Student Last Name","Mark");
                var studentMarks = _db.StudentMarks
                    .Include(b=>b.Student)
                    .Include(b=>b.Exam).ThenInclude(b=>b.Subject)
                    .ToList();
                foreach (var studentMark in studentMarks)
                {
                    table.AddRow(studentMark.Id, studentMark.ExamId,studentMark.Exam.Subject.Name,
                        studentMark.StudentId,studentMark.Student.FirstName,studentMark.Student.LastName,
                        studentMark.Mark);
                }
                Console.WriteLine("\n");
                Console.WriteLine(table.ToString());
                Console.WriteLine("\nEnter a Number to:");
                Console.WriteLine(
                    "0 - Back\n" +
                    "1 - Add a new Student Mark\n" +
                    "2 - Update a Student Mark\n" +
                    "3 - Delete a Student Mark\n"
                    );

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n" + ex.Message);
                }

                switch (selection)
                {
                    case 0:
                        return;
                    case 1:
                        AddStudentMark();
                        break;
                    case 2:
                        UpdateStudentMark();
                        break;
                    case 3:
                        DeleteStudentMark();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        private static void AddStudentMark()
        {
            Console.WriteLine();
            try
            {
                StudentMark std = new();
                Console.Write("Please Enter The Mark :  ");
                std!.Mark = Convert.ToInt32(Console.ReadLine()!);

                Console.WriteLine("Please Enter The Exam's Subject : \n ");
                var exams = _db.Exams.Include(b => b.Subject).ToList();
                foreach (var exam in exams)
                {
                    Console.WriteLine(exam.Id + "  -  " + exam.Subject?.Name);
                }
                Console.Write("\nChoose Exam's Subject ID From list above:  ");
                int d_id = Convert.ToInt32(Console.ReadLine());
                Exam? ex = _db.Exams.Include(b=>b.Subject).FirstOrDefault(s => s.Id == d_id)!;
                std!.ExamId =  ex.Id;

                Console.WriteLine("Please Enter The Student :  \n");
                var students = _db.Students
                    .Where(s => s.DepartmentId == ex.Subject.DepartmentId).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine(student.Id + "  -  " + student.Email);
                }
                Console.Write("\nChoose Student's ID From list above:  ");
                int s_id = Convert.ToInt32(Console.ReadLine());
                std!.StudentId = s_id;

                _db.StudentMarks.Add(std);
                _db.SaveChanges();
                Console.WriteLine("Student Mark Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }

        private static void Update()
        {
            Console.Write("Enter An ID From Table to Update its info:     ");
            int id = Convert.ToInt32(Console.ReadLine());
            StudentMark? std = _db.StudentMarks
                .Include(b=>b.Student)
                .Include(b=>b.Exam)
                .ThenInclude(b=>b.Subject)
                .FirstOrDefault(d => d.Id == id);
            while (true)
            {
                Console.WriteLine("Update:");
                Console.WriteLine(
                    "0 - Save in Database and Exit\n" +
                    "1 - Mark\n" +
                    "2 - Exam\n" +
                    "3 - Student\n");

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                    switch (selection)
                    {
                        case 0:
                            _db.StudentMarks.Update(std!);
                            Console.WriteLine("Student Mark Updated!");
                            _db.SaveChanges();
                            return;
                        case 1:
                            Console.Write("Please Enter The New Mark :  ");
                            std!.Mark =Convert.ToInt32(Console.ReadLine()!);
                            break;
                        case 2:
                            Console.WriteLine("Please Enter The New Exam's Subject : \n ");
                            var exams = _db.Exams.Include(b=>b.Subject)
                                .Where(b=>b.Subject.DepartmentId == std!.Student.DepartmentId)
                                .ToList();
                            foreach (var exam in exams)
                            {
                                Console.WriteLine(exam.Id + "  -  " + exam.Subject?.Name);
                            }
                            Console.Write("\nChoose Exam's Subject ID From list above:  ");
                            int d_id = Convert.ToInt32(Console.ReadLine());
                            std!.Exam = _db.Exams.FirstOrDefault(s => s.Id == d_id)!;
                            break;
                        case 3:
                            Console.WriteLine("Please Enter The New Student :  \n");
                            var students = _db.Students
                                .Where(s=>s.DepartmentId==std!.Exam.Subject.DepartmentId).ToList();
                            foreach (var student in students)
                            {
                                Console.WriteLine(student.Id + "  -  " + student.Email);
                            }
                            Console.Write("\nChoose Student's ID From list above:  ");
                            int s_id = Convert.ToInt32(Console.ReadLine());
                            std!.Student = _db.Students.FirstOrDefault(s => s.Id == s_id)!;
                            break;
                        default:
                            Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n" + ex.Message);
                }
            }
        }

        private static void Delete()
        {
            try
            {
                Console.Write("Enter An ID From Table to Delete it:     ");
                int id = Convert.ToInt32(Console.ReadLine());
                StudentMark? std = _db.StudentMarks.First(d => d.Id == id);
                _db.StudentMarks.Remove(std);
                _db.SaveChanges();
                Console.WriteLine("Student Mark Deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }
    }
}
