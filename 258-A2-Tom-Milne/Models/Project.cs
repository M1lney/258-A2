using System;
using System.ComponentModel.DataAnnotations;

//Model for Project
namespace _258_A2_Tom_Milne.Models
{
   
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string? UserId { get; set; } 
        public List<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>(); //list of tasks for the project
    }

}

