using Microsoft.AspNetCore.Http;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Interfaces
{
    public interface IAttachmentService
    {
        Task<Attachment> UploadFileAsync(IFormFile file, int taskId, string userId, string webRootPath);
        Task<IEnumerable<Attachment>> GetAttachmentsForTaskAsync(int taskId);
        Task<bool> DeleteAttachmentAsync(int attachmentId, string userId, string webRootPath);
    }
}
