using Microsoft.EntityFrameworkCore;
using SIMSWeb.ConstantsAndUtilities.AuthUtilities;
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
                        Password = PasswordUtility.HashPassword("Inco!@$"),
                        Role = "Admin",
                        CreatedAt = DateTime.Now,

                    },
                    new User
                    {
                        Id = 2,
                        Name = "Keya",
                        Email = "keya@gmail.com",
                        Password = PasswordUtility.HashPassword("stud"),
                        Role = "Student",
                        CreatedAt = DateTime.Now,
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Tiya",
                        Email = "tiya@gmail.com",
                        Password = PasswordUtility.HashPassword("staff"),
                        Role = "Teacher",
                        CreatedAt = DateTime.Now,
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Naveen",
                        Email = "nav@gmail.com",
                        Password = PasswordUtility.HashPassword("staff"),
                        Role = "Teacher",
                        CreatedAt = DateTime.Now,
                    },
                    new User
                    {
                        Id = 5,
                        Name = "Sid",
                        Email = "sid@gmail.com",
                        Password = PasswordUtility.HashPassword("stud"),
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

 
 

           
        }
    }
}
