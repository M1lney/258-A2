using System;
namespace _258_A2_Tom_Milne.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } 
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }

}

