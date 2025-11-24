using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Interfaces
{
    public interface INotifier
    {
        Task NotifyTaskCompletedAsync(AppTask task, string userId);
    }
}
