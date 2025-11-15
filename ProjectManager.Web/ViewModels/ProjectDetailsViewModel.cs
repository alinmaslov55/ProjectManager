using ProjectManager.Models;

namespace ProjectManager.Web.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public Project Project { get; set; } = null;
        public IEnumerable<AppTask> Tasks { get; set; } = new List<AppTask>();
    }
}
