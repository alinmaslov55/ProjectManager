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
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetCommentsWithUserAsync(int taskId)
        {
            return await Context.Set<Comment>()
                .Include(x => x.User)
                .Where(c => c.AppTaskId == taskId)
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }
    }
}
