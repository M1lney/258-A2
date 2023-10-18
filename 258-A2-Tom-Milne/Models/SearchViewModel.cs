using System;
namespace _258_A2_Tom_Milne.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public List<Project> Projects { get; set; }
        public List<ProjectTask> Tasks { get; set; }
    }
}

