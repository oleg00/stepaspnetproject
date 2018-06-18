using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppBlog.UI.Models
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength()]
        public string Content { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

    }
}