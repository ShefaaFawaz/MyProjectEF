using System;
using System.Collections.Generic;

namespace Project_EF.Models;

public partial class SubjectLecture
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
