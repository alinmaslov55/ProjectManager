using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Interfaces
{
    public interface IProjectService
    {
        Task<Project?> GetProjectByIdAsync(int projectId);
        Task<IEnumerable<Project>> GetProjectsForUserAsync(string userId);
        Task<Project> CreateProjectAsync(Project project, string ownerId);
        Task<bool> UpdateProjectAsync(Project project, string currentUserId);
        Task<bool> DeleteProjectAsync(int projectId, string currentUserId);
        Task<bool> IsUserOwnerAsync(int projectId, string userId);
        Task<Project?> GetProjectWithTasksAsync(int projectId, string userId);
    }
}
