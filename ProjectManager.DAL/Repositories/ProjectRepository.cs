using Microsoft.EntityFrameworkCore;
using ProjectManager.DAL.Repositories.IRepositories;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories
{
    public class ProjectRepository: Repository<Project>, IProjectRepository
    {
        public ApplicationDbContext AppContext => (Context as ApplicationDbContext)!;
        public ProjectRepository(DbContext context) : base(context)
        {
        }
        public async Task<Project?> GetProjectWithTasksAsync(int projectId)
        {
            return await AppContext.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }
    }
}
