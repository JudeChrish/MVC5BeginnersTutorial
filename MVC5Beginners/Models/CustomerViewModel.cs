using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Beginners.Models
{
    public class CustomerViewModel
    {        
        public int CustId { get; set; }
        [Required]
        [Display(Name ="Customer Name")]
        public string CustName { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public int CustMobileNo { get; set; }
        [Display(Name = "Address Line1")]
        public string CustAddress1 { get; set; }
        [Display(Name = "Address Line2")]
        public string CustAddress2 { get; set; }
        [Display(Name = "Address Line3")]
        public string CustAddress3 { get; set; }
    }
}