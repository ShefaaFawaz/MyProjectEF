using Project_EF.Data;
using Project_EF.Management;
using Project_EF.Models;

namespace Project_EF
{
    internal class Helper
    {
        public static void RunTCCManagement()
        {
            while (true)
            {
                Console.WriteLine("Enter a number to go to\n");
                Console.WriteLine(
                    "0 - Finish Execution\n" +
                    "1 - Department Management\n" +
                    "2 - Exam Management\n" +
                    "3 - Student Management\n" +
                    "4 - Student Mark Management\n" +
                    "5 - Subject Management\n" +
                    "6 - Subject Lectures Management\n" +
                    "7 - Reports Section\n");
                sbyte selection = -1;
                try
                {
                    selection = Convert.ToSByte(Console.ReadLine());

                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n"+ex.Message);
                }
                switch (selection)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("\n\n\n\tThanks For Using Our App\t\n\n\n");
                        return;
                    case 1:
                        DepartmentManagement.Management();
                        break;
                    case 2:
                        ExamManagement.Management();
                        break;
                    case 3:
                        StudentManagement.Management();
                        break;
                    case 4:
                        StudentMarkManagement.Management();
                        break;
                    case 5:
                        SubjectManagement.Management();
                        break;
                    case 6:
                        SubjectLecturesManagement.Management();
                        break;
                    case 7:
                        ReportManagement.Management();
                        break;
                    default:
                        Console.WriteLine("\n\nPlease Try Again with a valid number!\n");
                        break;
                }
            }
        }

        public static void SeedData()
        {
            var context = new ProjectDbContext();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Subjects.Any())
                return;
            context.Departments.AddRange(
                new Department()
                {
                    Name = "Software",
                    Students = new List<Student>()
                    {
                        new Student()
                        {
                            FirstName="sarah",
                            LastName = "habib",
                            DepartmentId = 1,
                            Email="sarahhabib@gmail.com",
                            Phone="0999333384",
                            RegisterDate= DateTime.Now,
                            Username="sarahtt"
                        },
                        new Student()
                        {
                            FirstName="massa",
                            LastName = "hala",
                            DepartmentId = 1,
                            Email="massa12@gmail.com",
                            Phone="0987654321",
                            RegisterDate= DateTime.Now
                        }
                    },
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name = "math 1",
                            DepartmentId=1,
                            Term = 1,
                            Year = 1,
                            MinimumDegree = 60
                        },
                        new Subject()
                        {
                            Name = "Advanced Programming 2",
                            DepartmentId=1,
                            Term = 2,
                            Year = 2,
                            MinimumDegree = 60
                        },
                        new Subject()
                        {
                            Name = "Advanced Programming 1",
                            DepartmentId=1,
                            Term = 2,
                            Year = 2,
                            MinimumDegree = 60
                        }
                    }
                },
                new Department()
                {
                    Name = "Network",
                    Students = new List<Student>()
                    {
                        new Student()
                        {
                            FirstName="samar",
                            LastName = "ee",
                            DepartmentId = 2,
                            Email="samar2@gmail.com",
                            Phone="098765432",
                            RegisterDate= DateTime.Now,
                            Username="samar1"
                        },
                        new Student()
                        {
                            FirstName="fadi",
                            LastName = "SS",
                            DepartmentId = 2,
                            Email="fadiKh1@gmail.com",
                            Phone="097856342",
                            RegisterDate= DateTime.Now
                        }
                    },
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name = "art",
                            DepartmentId=2,
                            Term = 2,
                            Year = 1,
                            MinimumDegree = 60
                        },
                        new Subject()
                        {
                            Name = "muisc",
                            DepartmentId=2,
                            Term = 1,
                            Year = 2,
                            MinimumDegree = 50
                        }
                    }
                },
                new Department()
                {
                    Name = "Computer",
                    Students = new List<Student>()
                    {
                        new Student()
                        {
                            FirstName="haneen",
                            LastName = "Kh",
                            DepartmentId = 3,
                            Email="haneenKh@gmail.com",
                            Phone="09876543",
                            RegisterDate= DateTime.Now,
                            Username="haneenKh"
                        },
                        new Student()
                        {
                            FirstName="hassan",
                            LastName = "ww",
                            DepartmentId = 3,
                            Email="hassanKh@gmail.com",
                            Phone="091527383",
                            RegisterDate= DateTime.Now
                        }
                    },
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name = "English 3",
                            DepartmentId=3,
                            Term = 2,
                            Year = 1,
                            MinimumDegree = 50
                        },
                        new Subject()
                        {
                            Name = "English 4",
                            DepartmentId=3,
                            Term = 1,
                            Year = 2,
                            MinimumDegree = 50
                        }
                    }
                });
            context.SaveChanges();
        }
    }
}
