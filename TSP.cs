namespace IDBeAcademy.Models
{
    public class TSP
    {
        public int TspId { get; set; }  
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int Phone { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>(); 
    }
}
