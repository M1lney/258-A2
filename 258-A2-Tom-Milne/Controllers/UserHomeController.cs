using _258_A2_Tom_Milne.Areas.Identity.Data;
using _258_A2_Tom_Milne.Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//Custom controller for handling actions related to the user home page. I used UserManager class from Identity to
//handle tracking logged in user. I then used this to populate a view model with projects associated with that user
//All actions only available to authenticated user
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

    //use UserManager to get lists of Projects and projecttasks with a userId matching the user
    public async Task<IActionResult> Index()
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        // Fetch user-specific data related to projects and ProjectTasks based on the id of logged in user, use linq to order by date 
        var userProjects = _a2DbContext.Project.Where(p => p.UserId == user.Id).OrderBy(p => p.Date).ToList();

        var userProjectTasks = userProjects
        .SelectMany(project => _a2DbContext.ProjectTask.Where(pt => pt.ProjectId == project.Id))
        .OrderBy(pt => pt.Date)
        .ToList();

        // Create a ViewModel to pass data to the view
        var viewModel = new UserHomeViewModel
        {
            Projects = userProjects,
            ProjectTasks = userProjectTasks
        };

        return View(viewModel);
    }

    //works in a similar way but takes a projectId variable to store a list of Project tasks with matching projectId
    public IActionResult ProjectDetails(int projectId)
    {
        var project = _a2DbContext.Project.Include(p => p.ProjectTasks).SingleOrDefault(p => p.Id == projectId);
        if (project == null)
        {
            return NotFound();
        }
        //create model using data from the project with the passed Id
        var viewModel = new ProjectViewModel
        {
            ProjectId = project.Id,
            Project = project,
            ProjectTasks = project.ProjectTasks.ToList()
        };

        return View(viewModel);
    }

    //Create a new SearchView model based on the searchTerm and SelectedFilter
    [Authorize]
    public async Task<IActionResult> Search(string searchTerm, string selectedFilter)
    {
        // Get the current user
        var user = await _userManager.GetUserAsync(User);

        // Filter matching projects and tasks by UserId
        var matchingProjects = _a2DbContext.Project
            .Where(p => p.UserId == user.Id && p.Title.Contains(searchTerm))
            .ToList();

        var matchingTasks = _a2DbContext.ProjectTask
            .Where(pt => pt.Project.UserId == user.Id && pt.Title.Contains(searchTerm))
            .ToList();

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


