using System;
namespace _258_A2_Tom_Milne.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; } // Foreign key to link the task to a project
        public Project Project { get; set; } // Navigation property to access the project
    }

}

