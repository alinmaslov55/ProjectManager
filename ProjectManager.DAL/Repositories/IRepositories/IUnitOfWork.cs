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
        Task<int> CompleteAsync(); // This will call SaveChangesAsync()
    }
}
