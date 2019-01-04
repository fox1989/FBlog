using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.Models
{
    public class User
    {
      
        public int Id { get; set; }
        [Required]
        [Display(Name ="用户名")]
        public string Name { get; set; }
        [Required]
        [StringLength(0,MinimumLength =6,ErrorMessage ="必须大于6位")]
        [Display(Name = "密码")]
        public string Pw { get; set; }
        public DateTime CreatTime { get; set; }

    }
}
