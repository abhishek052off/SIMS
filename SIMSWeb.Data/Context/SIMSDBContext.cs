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
             .OnDelete(DeleteBehavior.NoAction); ;

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
                .OnDelete(DeleteBehavior.NoAction); 


            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = 1,
                        Name = "Admin",
                        Email = "admin@gmail.com",
                        Password = "admin",
                        Role = "Admin"
                    },
                    new User
                    {
                        Id = 2,
                        Name = "Keya",
                        Email = "keya@gmail.com",
                        Password = "stud",
                        Role = "Student"
                    },
                    new User
                    {
                        Id = 3,
                        Name = "Tiya",
                        Email = "tiya@gmail.com",
                        Password = "staff",
                        Role = "Teacher"
                    },
                    new User
                    {
                        Id = 4,
                        Name = "Naveen",
                        Email = "nav@gmail.com",
                        Password = "staff",
                        Role = "Teacher"
                    },
                    new User
                    {
                        Id = 5,
                        Name = "Sid",
                        Email = "sid@gmail.com",
                        Password = "stud",
                        Role = "Student"
                    }
                  );

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 2, StudentName = "Keya" },
                new Student { Id = 2, UserId = 3, StudentName = "Sid" }
            );

            // Seed Teachers
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, UserId = 3, TeacherName = "Tiya", Department = "Mathematics" },
                new Teacher { Id = 2, UserId = 4, TeacherName = "Naveen", Department = "Physics" }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Algebra", TeacherId = 1 },
                new Course { Id = 2, Name = "Physics 101", TeacherId = 2 }
            );


            // Seed Many-to-Many Relationship (Enrollment)
            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { StudentId = 1, CourseId = 1 },  
                new Enrollment { StudentId = 1, CourseId = 2 },  
                new Enrollment { StudentId = 2, CourseId = 2 }   
            );
        }
    }
}
