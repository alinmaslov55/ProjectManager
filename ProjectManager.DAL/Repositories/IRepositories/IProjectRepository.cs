using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories.IRepositories
{
    public interface IProjectRepository: IRepository<Project>
    {
        Task<Project?> GetProjectWithTasksAsync(int projectId);
    }
}
