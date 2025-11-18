using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.BLL.Interfaces;
using ProjectManager.Models;
using ProjectManager.Web.ViewModels;
using System.Reflection;
using System.Security.Claims;

namespace ProjectManager.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IAppTaskService _taskService;

        public TasksController(IAppTaskService taskService)
        {
            _taskService = taskService;
        }
        private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // GET: AppTask/Create?projectId=?
        public IActionResult Create(int projectId)
        {
            return View(new TaskFormViewModel { ProjectId = projectId });
        }

        // POST: AppTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var task = new AppTask
            {
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId,
                DueDate = model.DueDate,
                Status = model.Status
            };

            var succes = await _taskService.CreateTaskAsync(task, GetUserId());
            if (!succes) return Forbid();

            return RedirectToAction("Details", "Projects", new { id = model.ProjectId });
        }


        // GET: AppTask/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            var model = new TaskFormViewModel
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = task.Status
            };
            return View(model);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var task = new AppTask
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.ProjectId,
                DueDate = model.DueDate,
                Status = model.Status
            };
            var succes = await _taskService.UpdateTaskAsync(task, GetUserId());
            if (!succes) return Forbid();
            return RedirectToAction("Details", "Projects", new { id = model.ProjectId });
        }


        // POST: Tasks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int projectId)
        {
            var success = await _taskService.DeleteTaskAsync(id, GetUserId());
            if (!success) return Forbid();

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }
    }
}
