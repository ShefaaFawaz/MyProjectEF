using System;
using System.Collections.Generic;

namespace Project_EF.Models;

public partial class StudentMark
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int ExamId { get; set; }

    public int Mark { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
