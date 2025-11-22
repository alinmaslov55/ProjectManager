using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories.IRepositories
{
    public interface ICommentRepository: ICommentRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsWithUserAsync(int taskId);
    }
}
