using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebAppBlog.UI.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string _CurrentUserName { get; set; }

        public int _Rate { get; set; }

        public bool _IsBlocked { get; set; }

        public string _OldUserName { get; set; }

        public ApplicationUser()
        {
            _OldUserName = base.Email;
            _Rate = 1;
            _IsBlocked = false;
        }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }

    }
}
