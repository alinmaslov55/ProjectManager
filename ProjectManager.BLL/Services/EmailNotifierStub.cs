using ProjectManager.BLL.Interfaces;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Services
{
    public class EmailNotifierStub : INotifier
    {
        public Task NotifyTaskCompletedAsync(AppTask task, string userId)
        {
            Debug.WriteLine($"[EMAIL SENT]: Task {task.Title} is complete! Emailing Project Owner...");
            return Task.CompletedTask;
        }
    }
}
