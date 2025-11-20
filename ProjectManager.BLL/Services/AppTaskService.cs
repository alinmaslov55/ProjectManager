using ProjectManager.BLL.Interfaces;
using ProjectManager.DAL.Repositories.IRepositories;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class AppTaskService : IAppTaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectService _projectService;


        public AppTaskService(IUnitOfWork unitOfWork, IProjectService projectService)
        {
            _unitOfWork = unitOfWork;
            _projectService = projectService;
        }
        public async Task<bool> CreateTaskAsync(AppTask task, string userId)
        {
            bool isOwner = await _projectService.IsUserOwnerAsync(task.ProjectId, userId);
            if (!isOwner)
            {
                return false;
            }

            await _unitOfWork.Tasks.AddAsync(task);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int taskId, string userId)
        {
            var task = await _unitOfWork.Tasks.GetAsync(taskId);
            if (task == null)
            {
                return false;
            }

            bool isOwner = await _projectService.IsUserOwnerAsync(task.ProjectId, userId);
            if (!isOwner)
            {
                return false;
            }
            _unitOfWork.Tasks.Remove(task);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<AppTask?> GetTaskByIdAsync(int taskId)
        {
            return await _unitOfWork.Tasks.GetAsync(taskId);
        }

        public async Task<bool> UpdateTaskAsync(AppTask task, string userId)
        {
            var existingTask = await _unitOfWork.Tasks.GetAsync(task.Id);

            if (existingTask == null)
            {
                return false;
            }

            bool isOwner = await _projectService.IsUserOwnerAsync(existingTask.ProjectId, userId);
            if (!isOwner)
            {
                return false;
            }
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;
            existingTask.DueDate = task.DueDate;

            await _unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<bool> UpdateTaskStatusAsync(int taskId, AppTaskStatus newStatus, string userId)
        {
            var task = await _unitOfWork.Tasks.GetAsync(taskId);
            if (task == null)
            {
                return false;
            }

            if(!await _projectService.IsUserOwnerAsync(task.ProjectId, userId))
            {
                return false;
            }

            task.Status = newStatus;
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
