using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories.IRepositories
{
    public interface IUnitOfWork: IDisposable
    {
        IProjectRepository Projects { get; }
        IAppTaskRepository Tasks { get; }
        IAttachmentRepository Attachments { get; }
        ICommentRepository Comments { get; }
        Task<int> CompleteAsync(); // This will call SaveChangesAsync()
    }
}
