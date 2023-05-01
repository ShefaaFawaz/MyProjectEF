using System;
using System.Collections.Generic;

namespace Project_EF.Models;

public partial class Subject
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public int MinimumDegree { get; set; }

    public short Term { get; set; }

    public short Year { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<SubjectLecture> SubjectLectures { get; set; } = new List<SubjectLecture>();
}
