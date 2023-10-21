using System;
namespace _258_A2_Tom_Milne.Models
{
	public class ProjectViewModel
	{
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public List<ProjectTask> ProjectTasks { get; set; }
        public ProjectTask NewProjectTask { get; set; }

    }

}

