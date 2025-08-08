using Microsoft.EntityFrameworkCore;
using SIMSWeb.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSWeb.Data.Context
{
    public class SIMSDBContext : DbContext
    {
        public SIMSDBContext(DbContextOptions<SIMSDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One to one relation with User and Student
            modelBuilder.Entity<User>()
                .HasOne(s => s.Student)
                .WithOne(u => u.User)
                .HasForeignKey<Student>(u => u.UserId);

            // One to one relation with User and Teacher
            modelBuilder.Entity<User>()
               .HasOne(t => t.Teacher)
               .WithOne(u => u.User)
               .HasForeignKey<Teacher>(t => t.UserId);

            // One to Many
            modelBuilder.Entity<Teacher>()
             .HasMany(t => t.Courses)
             .WithOne(c => c.Teacher)
             .HasForeignKey(t => t.TeacherId)
             .OnDelete(DeleteBehavior.NoAction);

            // Many to many, Student & Course
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(s => s.Student)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Enrollment>()
                .HasOne(s => s.Course)
                .WithMany(e => e.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        Name = "Admin",
                        Email = "admin@gmail.com",
                        Password = "admin",
                        Role = "Admin",
                        CreatedAt = DateTime.Now,

                    },
                    new User
                    {
                        Id = 2,
                        Name = "Keya",
                        Email = "keya@gmail.com",
                        Password = "stud",
                        Role = "Student",
                        CreatedAt = DateTime.Now,
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Tiya",
                        Email = "tiya@gmail.com",
                        Password = "staff",
                        Role = "Teacher",
                        CreatedAt = DateTime.Now,
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Naveen",
                        Email = "nav@gmail.com",
                        Password = "staff",
                        Role = "Teacher",
                        CreatedAt = DateTime.Now,
                    },
                    new User
                    {
                        Id = 5,
                        Name = "Sid",
                        Email = "sid@gmail.com",
                        Password = "stud",
                        Role = "Student",
                        CreatedAt = DateTime.Now,
                    }
                  );

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany(s => s.Submissions)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 2, EnrollmentDate = DateTime.Now },
                new Student { Id = 2, UserId = 3, EnrollmentDate = DateTime.Now }
            );

            // Seed Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 3, Department = "Mathematics", HireDate = DateTime.Now },
                new Teacher { Id = 2, UserId = 4, Department = "Physics", HireDate = DateTime.Now }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Algebra", TeacherId = 1, IsActive = true },
                new Course { Id = 2, Name = "Physics 101", TeacherId = 2, IsActive = true }
            );

            modelBuilder.Entity<Assignment>().HasData(
                new Assignment
                {
                    Id = 1,
                    Title = "Algebra Homework",
                    Description = "Solve all exercises in chapter 3",
                    MaxScore = 25,
                    DueDate = new DateTime(2025, 9, 1),
                    CourseId = 1
                },
                new Assignment
                {
                    Id = 2,
                    Title = "Lab Report",
                    Description = "Complete the lab report for week 1",
                    MaxScore = 50,
                    DueDate = new DateTime(2025, 9, 5),
                    CourseId = 11
                }
            );

            // Seed Submissions (must have valid AssignmentId)
            modelBuilder.Entity<Submission>().HasData(
                new Submission
                {
                    Id = 1,
                    StudentId = 1,
                    Score = 95,
                    SubmittedAt = new DateTime(2025, 8, 30),
                    AssignmentId = 1,
                    Feedback = "Well done"

                },
                new Submission
                {
                    Id = 2,
                    StudentId = 2,
                    Score = 88,
                    SubmittedAt = new DateTime(2025, 9, 1),
                    AssignmentId = 1
                },
                new Submission
                {
                    Id = 3,
                    StudentId = 1,
                    Score = 45,
                    SubmittedAt = new DateTime(2025, 9, 4),
                    AssignmentId = 2
                }
            );


            // Seed Many-to-Many Relationship (Enrollment)
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { StudentId = 1, CourseId = 1, Term = 1, Marks = 70 },
                new Enrollment { StudentId = 1, CourseId = 2, Term = 1, Marks = 65 },
                new Enrollment { StudentId = 2, CourseId = 2, Term = 1, Marks = 50 }
            );
        }
    }
}
