using ProjectManager.DAL.Repositories.IRepositories;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.DAL.Repositories
{
    public class AttachmentRepository: Repository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext context): base(context)
        {
        }
    }
}
