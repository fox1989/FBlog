using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    
    public class SignInFrom
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "必填")]
        [Remote(action: "VerifyUserName", controller: "Login")]
        public string Name { get; set; }

        [Display(Name = "密码")]
        [StringLength(12,MinimumLength =6,ErrorMessage = "大于6")]
        [Required(ErrorMessage = "必填")]
        [DataType(DataType.Password)]
        public string Pw { get; set; }


        [Display(Name = "重复密码")]
        [StringLength(12,MinimumLength =6,ErrorMessage ="大于6")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Pw",ErrorMessage ="密码不相同")]
        public string Cpw { get; set; }
    }

    public class LoginFrom
    {
        [Required]
        [Display(Name = "用户名")]
        public string Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "必须大于6位")]
        [Display(Name = "密码")]
        public string Pw { get; set; }

        public string returnUrl { get; set; }

    }


}
