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
    internal class SubjectManagement
    {
        static readonly ProjectDbContext _db = new();
        public static void Management()
        {
            while (true)
            {
                Table table = new("ID", "Name", "Term", "Year", "Minimum Degree", "Department");
                var subjects = _db.Subjects.Include(s => s.Department).ToList();
                foreach (var subject in subjects)
                {
                    table.AddRow(
                        subject.Id,subject.Name,subject.Term,subject.Year,subject.MinimumDegree,
                        subject.Department?.Name
                        );
                }
                Console.WriteLine("\n");
                Console.WriteLine(table.ToString());
                Console.WriteLine("\nEnter a Number to:");
                Console.WriteLine(
                    "0 - Back\n" +
                    "1 - Add a new Subject\n" +
                    "2 - Update a Subject\n" +
                    "3 - Delete a Subject\n"
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
                        AddSubject();
                        break;
                    case 2:
                        UpdateSubject();
                        break;
                    case 3:
                        DeleteSubject();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        private static void AddSubject()
        {
            Console.WriteLine();
            try
            {
                Subject subject = new();
                Console.Write("Please Enter The Name :  ");
                subject!.Name = Console.ReadLine()!;
                Console.Write("Please Enter The Term :  ");
                subject!.Term = Convert.ToInt16(Console.ReadLine()!);
                Console.Write("Please Enter The Year :  ");
                subject!.Year = Convert.ToInt16(Console.ReadLine()!);
                Console.Write("Please Enter The Minimum Degree :  ");
                subject!.MinimumDegree = Convert.ToInt16(Console.ReadLine()!);
                Console.WriteLine();
                var departments = _db.Departments.ToList();
                foreach (var department in departments)
                {
                    Console.WriteLine(department.Id + "  -  " + department.Name);
                }
                Console.Write("\nChoose Subject's Department ID From list above:  ");
                int d_id = Convert.ToInt32(Console.ReadLine());
                subject!.DepartmentId = d_id;
                _db.Subjects.Add(subject);
                _db.SaveChanges();
                Console.WriteLine("Subject Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }

        private static void UpdateSubject()
        {
            Console.Write("Enter An ID From Table to Update its info:     ");
            int id = Convert.ToInt32(Console.ReadLine());
            Subject? subject = _db.Subjects.FirstOrDefault(d => d.Id == id);
            while (true)
            {
                Console.WriteLine("Update:");
                Console.WriteLine(
                    "0 - Save in Database and Exit\n" +
                    "1 - Name\n" +
                    "2 - Term\n" +
                    "3 - Year\n" +
                    "4 - Minimum Degree\n" +
                    "5 - Department\n");

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                    switch (selection)
                    {
                        case 0:
                            _db.Subjects.Update(subject!);
                            Console.WriteLine("Subject Updated!");
                            _db.SaveChanges();
                            return;
                        case 1:
                            Console.Write("Please Enter The New Name :  ");
                            subject!.Name = Console.ReadLine()!;
                            break;
                        case 2:
                            Console.Write("Please Enter The New Term :  ");
                            subject!.Term =Convert.ToInt16(Console.ReadLine()!);
                            break;
                        case 3:
                            Console.Write("Please Enter The New Year :  ");
                            subject!.Year = Convert.ToInt16(Console.ReadLine()!);
                            break;
                        case 4:
                            Console.Write("Please Enter The New Minimum Degree :  ");
                            subject!.MinimumDegree = Convert.ToInt16(Console.ReadLine()!);
                            break;
                        case 5:
                            var departments = _db.Departments.ToList();
                            foreach (var department in departments)
                            {
                                Console.WriteLine(department.Id + "  -  " + department.Name);
                            }
                            Console.Write("\nChoose Student's Department ID From list above:  ");
                            int d_id = Convert.ToInt32(Console.ReadLine());
                            subject!.Department = _db.Departments.FirstOrDefault(s => s.Id == d_id)!;
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

        private static void DeleteSubject()
        {
            try
            {
                Console.Write("Enter An ID From Table to Delete it:     ");
                int id = Convert.ToInt32(Console.ReadLine());
                Subject? subject = _db.Subjects.First(d => d.Id == id);
                _db.Subjects.Remove(subject);
                _db.SaveChanges();
                Console.WriteLine("Subject Deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }
    }
}
