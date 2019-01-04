using Blog.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class Post
    {
        [Key]
        public int id { get; set; }

        public ApplicationUser author { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "标题")]
        public string title { get; set; }

        public string description { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "内容")]
        public string content { get; set; }

        public DateTime time { get; set; }





    }
}
