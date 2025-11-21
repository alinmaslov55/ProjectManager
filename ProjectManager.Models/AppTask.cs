using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class AppTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public AppTaskStatus Status { get; set; }
        public DateTime DueDate { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        // Foreign Key
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; } = null!;
    }
}
