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
    internal class StudentManagement
    {
        static readonly ProjectDbContext _db = new();
        public static void Management()
        {
            while (true)
            {
                Table table = new ("ID", "Username", "First Name", "Last Name"
                    , "Email", "Phone Number",
                    "Register Date", "Department"
                    );
                var students = _db.Students.Include(s => s.Department).ToList();
                foreach (var student in students)
                {
                    table.AddRow(
                        student.Id,student.Username?? "NULL",student.FirstName,student.LastName
                        , student.Email, student.Phone,
                        student.RegisterDate.ToString("yyyy-MM-dd"), student.Department?.Name ?? "NULL"
                        );
                }
                Console.WriteLine("\n");
                Console.WriteLine(table.ToString());
                Console.WriteLine("\nEnter a Number to:");
                Console.WriteLine(
                    "0 - Back\n" +
                    "1 - Add a new Student\n" +
                    "2 - Update a Student\n" +
                    "3 - Delete a Student\n"
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
                        AddStudent();
                        break;
                    case 2:
                        UpdateStudent();
                        break;
                    case 3:
                        DeleteStudent();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        private static void AddStudent()
        {
            Console.WriteLine();
            try
            {
                Student std = new();
                Console.Write("Please Enter The Username(Or Enter to skip it) :  ");
                string? username = Console.ReadLine();
                if (!username!.Trim().IsNullOrEmpty())
                    std!.Username = username;
                Console.Write("Please Enter The First Name :  ");
                std!.FirstName = Console.ReadLine()!;
                Console.Write("Please Enter The Last Name :  ");
                std!.LastName = Console.ReadLine()!;
                Console.Write("Please Enter The Email :  ");
                std!.Email = Console.ReadLine()!;
                Console.Write("Please Enter The Phone Number :  ");
                std!.Phone = Console.ReadLine()!;
                var departments = _db.Departments.ToList();
                foreach (var department in departments)
                {
                    Console.WriteLine(department.Id + "  -  " + department.Name);
                }
                Console.Write("\nChoose Student's Department ID From list above:  ");
                int d_id = Convert.ToInt32(Console.ReadLine());
                std!.DepartmentId = d_id;
                std.RegisterDate=DateTime.Now;
                _db.Students.Add(std);
                _db.SaveChanges();
                Console.WriteLine("Student Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }

        private static void UpdateStudent()
        {
            Console.Write("Enter An ID From Table to Update its info:     ");
            int id = Convert.ToInt32(Console.ReadLine());
            Student? std = _db.Students.FirstOrDefault(d => d.Id == id);
            while (true)
            {
                Console.WriteLine("Update:");
                Console.WriteLine(
                    "0 - Save in Database and Exit\n" +
                    "1 - Username\n" +
                    "2 - First Name\n" +
                    "3 - Last Name\n" +
                    "4 - Email\n" +
                    "5 - Phone Number\n" +
                    "6 - Department\n");

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                    switch (selection)
                    {
                        case 0:
                            _db.Students.Update(std!);
                            Console.WriteLine("Student Updated!");
                            _db.SaveChanges();
                            return;
                        case 1:
                            Console.Write("Please Enter The New Username :  ");
                            std!.Username = Console.ReadLine()!;
                            break;
                        case 2:
                            Console.Write("Please Enter The New First Name :  ");
                            std!.FirstName = Console.ReadLine()!;
                            break;
                        case 3:
                            Console.Write("Please Enter The New Last Name :  ");
                            std!.LastName = Console.ReadLine()!;
                            break;
                        case 4:
                            Console.Write("Please Enter The New Email :  ");
                            std!.Email = Console.ReadLine()!;
                            break;
                        case 5:
                            Console.Write("Please Enter The New Phone Number :  ");
                            std!.Phone = Console.ReadLine()!;
                            break;
                        case 6:
                            var departments = _db.Departments.ToList();
                            foreach (var department in departments)
                            {
                                Console.WriteLine(department.Id + "  -  " + department.Name);
                            }
                            Console.Write("\nChoose Student's Department ID From list above:  ");
                            int d_id = Convert.ToInt32(Console.ReadLine());
                            std!.Department = _db.Departments.FirstOrDefault(s => s.Id == d_id)!;
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

        private static void DeleteStudent()
        {
            try
            {
                Console.Write("Enter An ID From Table to Delete it:     ");
                int id = Convert.ToInt32(Console.ReadLine());
                Student? std = _db.Students.First(d => d.Id == id);
                _db.Students.Remove(std);
                _db.SaveChanges();
                Console.WriteLine("Student Deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }
    }
}
