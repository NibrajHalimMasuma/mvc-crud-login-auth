namespace IDBeAcademy.Models
{
    public class Trainee
    {
        public int TraineeId { get; set; }  
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
       
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; } 
        public int TraineeId { get; set; }  
        public int CourseId { get; set; }  
    }
}

