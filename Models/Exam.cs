using System;
using System.Collections.Generic;

namespace Project_EF.Models;

public partial class Exam
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public DateTime Date { get; set; }

    public short Term { get; set; }

    public virtual ICollection<StudentMark> StudentMarks { get; set; } = new List<StudentMark>();

    public virtual Subject Subject { get; set; } = null!;
}
