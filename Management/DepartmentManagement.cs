using BetterConsoleTables;
using Project_EF.Data;
using Project_EF.Models;

namespace Project_EF.Management
{
    internal class DepartmentManagement
    {

        static readonly ProjectDbContext _db = new();
        public static void Management()
        {
            while (true)
            {
                Table table = new("ID", "Name");
                var departments = _db.Departments.ToList();
                foreach (var department in departments)
                {
                    table.AddRow(department.Id, department.Name);
                }
                Console.WriteLine("\n");
                Console.WriteLine(table.ToString());
                Console.WriteLine("\nEnter a Number to:");
                Console.WriteLine(
                    "0 - Back\n" +
                    "1 - Add a new Department\n" +
                    "2 - Update a Department\n" +
                    "3 - Delete a Department\n"
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
                        AddDepartment();
                        break;
                    case 2:
                        UpdateDepartment();
                        break;
                    case 3:
                        DeleteDepartment();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        private static void AddDepartment()
        {
            Console.WriteLine();
            try
            {
                Console.Write("Please Enter The Name :  ");
                Department dept = new()
                {
                    Name = Console.ReadLine()!
                };
                _db.Departments.Add(dept);
                _db.SaveChanges();
                Console.WriteLine("Department Added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }

        private static void UpdateDepartment()
        {
            Console.Write("Enter An ID From Table to Update its info:     ");
            int id = Convert.ToInt32(Console.ReadLine());
            Department? dept = _db.Departments.FirstOrDefault(d => d.Id == id);
            while (true)
            {
                Console.WriteLine("Update:");
                Console.WriteLine(
                    "0 - Save in Database and Exit\n" +
                    "1 - Name\n");

                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());
                    switch (selection)
                    {
                        case 0:
                            _db.Departments.Update(dept!);
                            Console.WriteLine("Department Updated!");
                            _db.SaveChanges();
                            return;
                        case 1:
                            Console.Write("Enter The New Name:  ");
                            dept!.Name = Console.ReadLine()!;
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

        private static void DeleteDepartment()
        {
            try
            {
                Console.Write("Enter An ID From Table to Delete it:    ");
                int id = Convert.ToInt32(Console.ReadLine());
                Department? dept = _db.Departments.First(d => d.Id == id);
                _db.Departments.Remove(dept);
                _db.SaveChanges();
                Console.WriteLine("Department Deleted!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
            }
        }
    }
}
