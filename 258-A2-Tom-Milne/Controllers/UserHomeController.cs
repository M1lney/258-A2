// UserHomeController.cs
using _258_A2_Tom_Milne.Areas.Identity.Data;
using _258_A2_Tom_Milne.Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Authorize]
public class UserHomeController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly A2DbContext _a2DbContext;

    public UserHomeController(UserManager<IdentityUser> userManager,  A2DbContext a2DbContext)
    {
        _userManager = userManager;
        _a2DbContext = a2DbContext;
    }

    public async Task<IActionResult> Index()
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        // Fetch user-specific data related to projects and ProjectTasks, use linq to order by date 
        var userProjects = _a2DbContext.Project.Where(p => p.UserId == user.Id).OrderBy(p => p.Date).ToList();
        var userProjectTasks = _a2DbContext.ProjectTask.Where(pt => pt.UserId == user.Id).OrderBy(pt => pt.Date).ToList();

        // Create a ViewModel to pass data to the view
        var viewModel = new UserHomeViewModel
        {
            Projects = userProjects,
            ProjectTasks = userProjectTasks
        };

        return View(viewModel);
    }

    public IActionResult ProjectDetails(int projectId)
    {
        var project = _a2DbContext.Project.Include(p => p.ProjectTasks).SingleOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            return NotFound();
        }

        var viewModel = new ProjectViewModel
        {
            ProjectId = project.Id,
            Project = project,
            ProjectTasks = project.ProjectTasks.ToList()
        };

        return View(viewModel);
    }

    public IActionResult Search(string searchTerm, string selectedFilter)
    {
        

        var matchingProjects = _a2DbContext.Project.Where(p => p.Title.Contains(searchTerm)).ToList();
        var matchingTasks = _a2DbContext.ProjectTask.Where(pt => pt.Title.Contains(searchTerm)).ToList();

        // Create a view model to display the search results
        var viewModel = new SearchViewModel
        {
            SearchTerm = searchTerm,
            filterTerm = selectedFilter,
            Projects = matchingProjects,
            Tasks = matchingTasks
        };

        return View("SearchResult", viewModel);
    }
    
}


