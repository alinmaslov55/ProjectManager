using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.BLL.Interfaces;
using ProjectManager.Models;
using ProjectManager.Web.ViewModels;
using System.Security.Claims;

namespace ProjectManager.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectsController(IProjectService projectService, UserManager<ApplicationUser> userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
        }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // GET: Projects/(Index)
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsForUserAsync(GetCurrentUserId());

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Title = viewModel.Title,
                    Description = viewModel.Description
                };
                await _projectService.CreateProjectAsync(project, GetCurrentUserId());
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // Get: Projects/Edit/id=?
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null || project.OwnerId != GetCurrentUserId())
            {
                return NotFound();
            }

            var viewModel = new ProjectEditViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectEditViewModel viewModel)
        {
            if(id != viewModel.Id)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                var projectToUpdate = new Project
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Description = viewModel.Description
                };

                var success = await _projectService.UpdateProjectAsync(projectToUpdate, GetCurrentUserId());

                if (!success)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Projects/Delete/id=?

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            
            if(project == null || project.OwnerId != GetCurrentUserId())
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _projectService.DeleteProjectAsync(id, GetCurrentUserId());
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectWithTasksAsync(id, GetCurrentUserId());
            if (project == null)
            {
                return NotFound();
            }

            var viewModel = new ProjectDetailsViewModel
            {
                Project = project,
                Tasks = project.Tasks.OrderBy(t => t.Status)
            };
            return View(viewModel);
        }
    }
}
