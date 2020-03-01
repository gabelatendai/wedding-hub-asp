using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WeddingHub.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }
    public partial class UserMetadata
    {

     
        [Required(AllowEmptyStrings =false, ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Please Enter Email ID")]
        [Display(Name = "Email ID")]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

         [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:MM/dd/yyyy}")]
        public string DateOfBirth { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password", AllowEmptyStrings =false)]
        [Display(Name = "Password")]
        [MinLength(6,ErrorMessage ="Minimum 6 characters required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        [Compare("Password",ErrorMessage ="Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

         
        [Required(ErrorMessage = "Please Select the Purpose Of your Business", AllowEmptyStrings = false)]
        [Display(Name = "Purpose")]
        public string Purpose { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}