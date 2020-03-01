using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

//using MySql.Data.MySqlClient;

namespace WeddingHub.Models
{
    public class Vendors
    {
       public int id { get; set; }
      
        [Required(ErrorMessage = "Please Enter Bussiness Name", AllowEmptyStrings = false)]
        [Display(Name = "Business Name")]
        public string bzname { get; set; }
        [Required]
       public HttpPostedFileWrapper photo { get; set; }
        [Required(ErrorMessage = "Please Enter Bussiness Description", AllowEmptyStrings = false)]
        [Display(Name = "Business Description")]
        public string description { get; set; }
        [Required(ErrorMessage = "Please Enter Bussiness Purpose", AllowEmptyStrings = false)]
        [Display(Name = "Business Purpose")]
        public string purpose { get; set; }
        [Required(ErrorMessage = "Please Enter Bussiness Address", AllowEmptyStrings = false)]
        [Display(Name = "Business Address")]
        public string address { get; set; }
        [Required(ErrorMessage = "Please Enter Pgone number", AllowEmptyStrings = false)]
        [Display(Name = "Contact Number")]
        public string phone { get; set; }
       public int paid { get; set; }
       [Display(Name = "Facebook")]
        public string facebook { get; set; }
        [Display(Name = "Twitter")]
        public string twitter { get; set; }
        [Display(Name = "Instagram")]
        public string instagram { get; set; }
         [Display(Name = "Youtube")]
        public string youtube { get; set; }
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime regdate { get; set; }
       public int user_id { get; set; }
    }
}