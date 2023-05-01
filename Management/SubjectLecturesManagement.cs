using BetterConsoleTables;
using Microsoft.EntityFrameworkCore;
using Project_EF.Data;
using Project_EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project_EF.Management
{
    internal class SubjectLecturesManagement
    {
        static readonly ProjectDbContext _db = new();
        public static void Management()
        {
            while (true)
            {
                Table table = new("ID", "Subject Name", "Title", "Content");
                var subjectLectures = _db.SubjectLectures.Include(s => s.Subject).ToList();
                foreach (var lecture in subjectLectures)
                {
                    table.AddRow(lecture.Id, lecture.Subject?.Name,lecture.Title,lecture.Content ?? "");
                }
                Console.WriteLine("\n");
                Console.WriteLine(table.ToString());
                Console.WriteLine("\nEnter a Number to:");
                Console.WriteLine(
                    "0 - Back\n" +
                    "1 - Add a new Lecture\n" +
                    "2 - Update a Lecture\n" +
                    "3 - Delete a Lecture\n"
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
                        AddLecture();
                        break;
                    case 2:
                        UpdateLecture();
                        break;
                    case 3:
                        DeleteLecture();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        private static void AddLecture()
        {
            Console.WriteLine();
            try
            {
                SubjectLecture lecture = new();
                Console.Write("Please Enter The Title :  ");
                lecture.Title = Console.ReadLine()!;
                Console.Write("Please Enter The Content :  ");
                lecture.Content = Console.ReadLine()!;
                Console.WriteLine();
                var subjects = _db.Subjects.ToList();
                foreach (var subject in subjects)
                {
                    Console.WriteLine(subject.Id + "  -  " + subject.Name);
                }
                Console.Write("\nChoose Lecture's Subject ID From list above:  ");
                lecture.SubjectId = Convert.ToInt32(Console.ReadLine());
                _db.SubjectLectures.Add(lecture);
                _db.SaveChanges();
                Console.WriteLine("Lecture Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }

        private static void UpdateLecture()
        {
            Console.Write("Enter An ID From Table to Update its info:     ");
            int id = Convert.ToInt32(Console.ReadLine());
            SubjectLecture? lecture = _db.SubjectLectures.FirstOrDefault(d => d.Id == id);
            while (true)
            {
                Console.WriteLine("Update:");
                Console.WriteLine(
                    "0 - Save in Database and Exit\n" +
                    "1 - Title\n" +
                    "2 - Content\n" +
                    "3 - Subject\n");

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                    switch (selection)
                    {
                        case 0:
                            _db.SubjectLectures.Update(lecture!);
                            Console.WriteLine("Lecture Updated!");
                            _db.SaveChanges();
                            return;
                        case 1:
                            Console.Write("Please Enter The new Title :  ");
                            lecture!.Title = Console.ReadLine()!;
                            break;
                        case 2:
                            Console.Write("Please Enter The New Content :  ");
                            lecture!.Content = Console.ReadLine()!;
                            break;
                        case 3:
                            var subjects = _db.Subjects.ToList();
                            foreach (var subject in subjects)
                            {
                                Console.WriteLine(subject.Id + "  -  " + subject.Name);
                            }
                            Console.Write("\nChoose Lecture's Subject ID From list above:  ");
                            int s_id = Convert.ToInt32(Console.ReadLine());
                            lecture!.Subject = _db.Subjects.FirstOrDefault(s => s.Id == s_id)!;
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

        private static void DeleteLecture()
        {
            try
            {
                Console.Write("Enter An ID From Table to Delete it:     ");
                int id = Convert.ToInt32(Console.ReadLine());
                SubjectLecture? lecture = _db.SubjectLectures.First(d => d.Id == id);
                _db.SubjectLectures.Remove(lecture);
                _db.SaveChanges();
                Console.WriteLine("Lecture Deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }
    }
}
