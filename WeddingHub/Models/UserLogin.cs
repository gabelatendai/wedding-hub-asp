using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WeddingHub.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Email ID")]
        [Display(Name = "Email ID")]
         public string EmailID { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password", AllowEmptyStrings = false)]
        [Display(Name = "Password")]
         public string Password { get; set; }

        [Display(Name = "Remember Me")]
         public bool RememberMe { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}