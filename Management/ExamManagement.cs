using BetterConsoleTables;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EF.Management
{
    internal class ExamManagement
    {
        static readonly ProjectDbContext _db = new();
        public static void Management()
        {
            while (true)
            {
                Table table = new("Exam ID", "Subject ID","Subject Name","Date","Term");
                var exams = _db.Exams.Include(s=>s.Subject).ToList();
                foreach (var exam in exams)
                {
                    table.AddRow(exam.Id, exam.Subject.Id, exam.Subject.Name, exam.Date.ToString("yyyy-MM-dd"), exam.Term);
                }
                Console.WriteLine("\n");
                Console.WriteLine(table.ToString());
                Console.WriteLine("\nEnter a Number to:");
                Console.WriteLine(
                    "0 - Back\n" +
                    "1 - Add a new Exam\n" +
                    "2 - Update an Exam\n" +
                    "3 - Delete an Exam\n"
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
                        AddExam();
                        break;
                    case 2:
                        UpdateExam();
                        break;
                    case 3:
                        DeleteExam();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        private static void AddExam()
        {
            Console.WriteLine();
            try
            {
                Exam exam = new();
                Console.Write("Please Enter The Date (yyyy-MM-dd) :  ");
                exam.Date = Convert.ToDateTime(Console.ReadLine()!);
                Console.Write("Please Enter The Term :  ");
                exam.Term = Convert.ToInt16(Console.ReadLine()!);
                Console.WriteLine();
                var subjects = _db.Subjects.ToList();
                foreach (var subject in subjects)
                {
                    Console.WriteLine(subject.Id + "  -  " + subject.Name);
                }
                Console.Write("\nChoose Exam's Subject ID From list above:  ");
                exam.SubjectId = Convert.ToInt32(Console.ReadLine());
                _db.Exams.Add(exam);
                _db.SaveChanges();
                Console.WriteLine("Exam Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }

        private static void UpdateExam()
        {
            Console.Write("Enter An ID From Table to Update its info:     ");
            int id = Convert.ToInt32(Console.ReadLine());
            Exam? exam = _db.Exams.FirstOrDefault(d => d.Id == id);
            while (true)
            {
                Console.WriteLine("Update:");
                Console.WriteLine(
                    "0 - Save in Database and Exit\n" +
                    "1 - Term\n" +
                    "2 - Date\n" +
                    "3 - Subject\n");

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                    switch (selection)
                    {
                        case 0:
                            _db.Exams.Update(exam!);
                            Console.WriteLine("Exam Updated!");
                            _db.SaveChanges();
                            return;
                        case 1:
                            Console.Write("Please Enter The New Term :  ");
                            exam!.Term = Convert.ToInt16(Console.ReadLine()!);
                            break;
                        case 2:
                            Console.Write("Please Enter The New Date (yyyy-MM-dd) :  ");
                            exam!.Date = Convert.ToDateTime(Console.ReadLine()!);
                            break;
                        case 3:
                            var subjects = _db.Subjects.ToList();
                            foreach (var subject in subjects)
                            {
                                Console.WriteLine(subject.Id + "  -  " + subject.Name);
                            }
                            Console.Write("\nChoose Exam's Subject ID From list above:  ");
                            int s_id = Convert.ToInt32(Console.ReadLine());
                            exam!.Subject = _db.Subjects.FirstOrDefault(s => s.Id == s_id)!;
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

        private static void DeleteExam()
        {
            try
            {
                Console.Write("Enter An ID From Table to Delete it:     ");
                int id = Convert.ToInt32(Console.ReadLine());
                Exam? exam = _db.Exams.First(d => d.Id == id);
                _db.Exams.Remove(exam);
                _db.SaveChanges();
                Console.WriteLine("Exam Deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
            }
        }
    }
}
