using System.ComponentModel.DataAnnotations;
using ProjectManager.Models;

namespace ProjectManager.Web.ViewModels
{
    public class TaskFormViewModel
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; } = DateTime.Today;

        [Required]
        public AppTaskStatus Status { get; set; }

        public IEnumerable<Attachment> Attachments { get; set; } = new List<Attachment>();
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
