using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadDate { get; set; }
        public string UploadedById { get; set; } = string.Empty;

        // Foreign key to Task
        public int AppTaskId { get; set; }
        public virtual AppTask AppTask { get; set; } = null!;
    }
}
