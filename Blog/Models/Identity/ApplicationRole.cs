using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Identity
{
    public class ApplicationRole: IdentityRole<int>
    {


        public virtual ICollection<ApplicationRole> UserRoles { get; set; }

    }
}
