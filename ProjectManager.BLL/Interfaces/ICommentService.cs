using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> AddCommentAsync(int tasId, string content, string userId);
        Task<IEnumerable<Comment>> GetCommentsForTaskAsync(int tasId);
    }
}
