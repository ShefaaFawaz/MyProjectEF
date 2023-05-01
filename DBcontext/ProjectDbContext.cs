using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Project_EF.Models;

namespace Project_EF.Data;

public partial class ProjectDbContext : DbContext
{

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentMark> StudentMarks { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<SubjectLecture> SubjectLectures { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=AdvancedProgramming2; Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RegisterDate).HasColumnType("date");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Students)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StudentMark>(entity =>
        {
            entity.ToTable("StudentMark");

            entity.HasOne(d => d.Exam).WithMany(p => p.StudentMarks)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Student).WithMany(p => p.StudentMarks)
                .HasForeignKey(d => d.StudentId);
               
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.DepartmentId);
        });

        modelBuilder.Entity<SubjectLecture>(entity =>
        {
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Subject).WithMany(p => p.SubjectLectures)
                .HasForeignKey(d => d.SubjectId);
        });
    }
}
