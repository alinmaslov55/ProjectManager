using Microsoft.AspNetCore.Http;
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
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttachmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> DeleteAttachmentAsync(int attachmentId, string userId, string webRootPath)
        {
            var attachment = await _unitOfWork.Attachments.GetAsync(attachmentId);

            if (attachment == null)
            {
                return false;
            }

            string relativePath = attachment.FilePath.TrimStart('/');
            string fullPath = Path.Combine(webRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            _unitOfWork.Attachments.Remove(attachment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<Attachment>> GetAttachmentsForTaskAsync(int taskId)
        {
            return await _unitOfWork.Attachments.FindAsync(a => a.AppTaskId == taskId);
        }

        public async Task<Attachment> UploadFileAsync(IFormFile file, int taskId, string userId, string webRootPath)
        {
            string uploadsFolder = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            var attachment = new Attachment
            {
                FileName = file.FileName,
                FilePath = "/uploads/" + uniqueFileName,
                AppTaskId = taskId,
                UploadedById = userId,
                UploadDate = DateTime.Now
            };

            await _unitOfWork.Attachments.AddAsync(attachment);
            await _unitOfWork.CompleteAsync();

            return attachment;
        }

    }
}
