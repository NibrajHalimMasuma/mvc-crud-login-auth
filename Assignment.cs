namespace IDBeAcademy.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }  
        public string? Title { get; set; }  
        public string? Description { get; set; }  
        public DateTime DueDate { get; set; }  
        public int CourseId { get; set; }  

        public Course? Course { get; set; }  
    }
    public class Submission
    {
        public int SubmissionId { get; set; }  
        public int AssignmentId { get; set; }  
        public int TraineeId { get; set; }  
        public DateTime SubmissionDate { get; set; }  

        
        public Assignment? Assignment { get; set; }  
        public Trainee? Trainee { get; set; }  
    }
}
