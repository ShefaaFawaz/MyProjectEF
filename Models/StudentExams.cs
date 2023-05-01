using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EF.Models
{
    internal class StudentExams
    {
        public int Id { get; set; }

        public int ExamId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string ExamSubjectName { get; set; } = null!;

        public StudentExams(Student s,int exam_id,string subject)
        {
            Id = s.Id;
            FirstName = s.FirstName;
            LastName = s.LastName;
            ExamId = exam_id;
            ExamSubjectName = subject;
        }

    }
}
