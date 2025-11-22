using ProjectManager.DAL.Repositories.IRepositories;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProjectRepository Projects { get; private set; }

        public IAppTaskRepository Tasks { get; private set; }

        public IAttachmentRepository Attachments { get; private set; }

        public ICommentRepository Comments { get; private set; }

        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Projects = new ProjectRepository(_context);
            Tasks = new AppTaskRepository(_context);
            Attachments = new AttachmentRepository(_context);
            Comments = new CommentRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
