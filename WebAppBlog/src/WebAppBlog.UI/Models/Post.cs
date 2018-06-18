using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppBlog.UI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string ShortContent { get; set; }

        [Required]
        [MaxLength(500)]
        public string FullContent { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Comment> Comments { get; set; }

    }


}
