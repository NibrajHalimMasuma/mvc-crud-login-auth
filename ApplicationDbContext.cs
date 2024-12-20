

using IDBeAcademy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IDBeAcademy.Data
{
    public class AppUser : IdentityUser<long>
    {

        public string? ProfilePicture { get; set; }
    }

    public class UserRole : IdentityRole<long>
    {

    }
    public class ApplicationDbContext : IdentityDbContext<AppUser, UserRole, long>

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<TSP> TSPs { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<TSP>().HasData(
                new TSP { TspId = 1, Name = "Tech Academy", Location = "Chittagong", Phone = 123456789 },
                new TSP { TspId = 2, Name = "Code Academy", Location = "Dhaka", Phone = 987654321 }
            );

            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { TrainerId = 1, FirstName = "Saima", LastName = "Afreen", Email = "Saimaafreen@gmail.com" },
                new Trainer { TrainerId = 2, FirstName = "Jany", LastName = "fareen", Email = "janyfareen@gmail.com" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, Title = "C# Basics", Description = "Learn the fundamentals of C#", TspId = 1, TrainerId = 1 },
                new Course { CourseId = 2, Title = "ASP.NET Core", Description = "Learn web development with ASP.NET Core", TspId = 2, TrainerId = 2 }
            );

            modelBuilder.Entity<Trainee>().HasData(
                new Trainee { TraineeId = 1, FirstName = "Aliya", LastName = "Ismat", Email = "Aliyaismat@gmail.com", Password = "password123", EnrollmentDate = DateTime.Now },
                new Trainee { TraineeId = 2, FirstName = "Boby", LastName = "Rahman", Email = "bobyrahman@gmail.com", Password = "password123", EnrollmentDate = DateTime.Now }
            );

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment { EnrollmentId = 1, TraineeId = 1, CourseId = 1 },
                new Enrollment { EnrollmentId = 2, TraineeId = 2, CourseId = 2 }
            );

            modelBuilder.Entity<Assignment>().HasData(
                new Assignment { AssignmentId = 1, Title = "C# Assignment 1", Description = "Complete the basic C# exercises", DueDate = DateTime.Now.AddDays(7), CourseId = 1 },
                new Assignment { AssignmentId = 2, Title = "ASP.NET Core Assignment 1", Description = "Complete the basic ASP.NET Core exercises", DueDate = DateTime.Now.AddDays(10), CourseId = 2 }
            );

            modelBuilder.Entity<Submission>().HasData(
                new Submission { SubmissionId = 1, AssignmentId = 1, TraineeId = 1, SubmissionDate = DateTime.Now.AddDays(2) },
                new Submission { SubmissionId = 2, AssignmentId = 2, TraineeId = 2, SubmissionDate = DateTime.Now.AddDays(5) }
            );
        }
    }
}
