using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Interfaces
{
    public interface IAppTaskService
    {
        Task<AppTask?> GetTaskByIdAsync(int taskId);
        Task<bool> CreateTaskAsync(AppTask task, string userId);
        Task<bool> UpdateTaskAsync(AppTask task, string userId);
        Task<bool> DeleteTaskAsync(int taskId, string userId);
    }
}
