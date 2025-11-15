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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> CreateProjectAsync(Project project, string ownerId)
        {
            project.OwnerId = ownerId;
            project.CreatedDate = DateTime.UtcNow;
            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.CompleteAsync();
            return project;
        }

        public async Task<bool> DeleteProjectAsync(int projectId, string currentUserId)
        {
            var project = await _unitOfWork.Projects.GetAsync(projectId);
            if (project == null || project.OwnerId != currentUserId)
            {
                return false;
            }

            _unitOfWork.Projects.Remove(project);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _unitOfWork.Projects.GetAsync(projectId);
        }

        public async Task<IEnumerable<Project>> GetProjectsForUserAsync(string userId)
        {
            return await _unitOfWork.Projects.FindAsync(p => p.OwnerId == userId);
        }

        public async Task<Project?> GetProjectWithTasksAsync(int projectId, string userId)
        {
            var project = await _unitOfWork.Projects.GetProjectWithTasksAsync(projectId);

            if (project == null || project.OwnerId != userId)
            {
                return null;
            }

            return project;
        }

        public async Task<bool> IsUserOwnerAsync(int projectId, string userId)
        {
            var project = await _unitOfWork.Projects.GetAsync(projectId);
            return project != null && project.OwnerId == userId;
        }

        public async Task<bool> UpdateProjectAsync(Project project, string currentUserId)
        {
            var exitingProject = await _unitOfWork.Projects.GetAsync(project.Id);
            if (exitingProject == null || exitingProject.OwnerId != currentUserId)
            {
                return false;
            }

            exitingProject.Title = project.Title;
            exitingProject.Description = project.Description;

            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
