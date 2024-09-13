using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class RegisterViewModels
    {
        [MaxLength(200)]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter You {0}")]
        public string Name { get; set; }

        [MaxLength(300)]
        [EmailAddress]
        [Display(Name ="Email")]
        [Required(ErrorMessage = "Please Enter You {0}")]
        [Remote("VerifyEmail","Account")]
        public string Email { get; set; }

        
        [MaxLength(10)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter You {0}")]
        public string Password { get; set; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Repeat Password")]
        [Required(ErrorMessage = "Please Enter You {0}")]
        public string RePassword { get; set; }
    }


    public class LoginViewModel
    {
        [MaxLength(300)]
        [EmailAddress]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter You {0}")]
        public string Email { get; set; }


        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter You {0}")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
