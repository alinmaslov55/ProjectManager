using ProjectManager.BLL.Interfaces;
using ProjectManager.DAL;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class DbNotifier : INotifier
    {
        private readonly ApplicationDbContext _context;

        public DbNotifier(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task NotifyTaskCompletedAsync(AppTask task, string userId)
        {
            var project = await _context.Projects.FindAsync(task.ProjectId);
            if (project == null)
            {
                return;
            }

            var notification = new Notification
            {
                UserId = project.OwnerId,
                Message = $"Task '{task.Title}' in project '{project.Title}' was marked as Done by {userId}.",
                CreatedDate = DateTime.Now,
                RelatedProjectId = project.Id,
                IsRead = false
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}
