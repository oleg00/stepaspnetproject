using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppBlog.UI.Models;

namespace WebAppBlog.UI.ViewModels
{
    public class PostViewModel
    {

        public List<Post> Posts { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public string TextAreaComment { get; set; }

        public List<ApplicationUser> Users { get; set; }

    }
}
