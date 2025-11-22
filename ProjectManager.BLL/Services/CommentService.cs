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
    public class CommentService: ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Comment> AddCommentAsync(int taskId, string content, string userId)
        {
            var comment = new Comment
            {
                AppTaskId = taskId,
                Content = content,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            return comment;
        }

        public async Task<IEnumerable<Comment>> GetCommentsForTaskAsync(int taskId)
        {
            return await _unitOfWork.Comments.GetCommentsWithUserAsync(taskId);
        }
    }
}
