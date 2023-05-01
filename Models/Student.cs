using System;
using System.Collections.Generic;

namespace Project_EF.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime RegisterDate { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<StudentMark> StudentMarks { get; set; } = new List<StudentMark>();
}
