using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Models
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
