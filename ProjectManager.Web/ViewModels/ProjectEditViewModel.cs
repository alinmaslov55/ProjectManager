using System.ComponentModel.DataAnnotations;
namespace ProjectManager.Web.ViewModels
{
    public class ProjectEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
    }
}
