using Microsoft.AspNetCore.Authorization;

namespace IDBeAcademy.Models
{
    public class Course
    {
        public int CourseId { get; set; }  
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TspId { get; set; }  
        public int TrainerId { get; set; }  

        public TSP? TSP { get; set; }  
        public Trainer? Trainer { get; set; }  

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();  
    }
}
