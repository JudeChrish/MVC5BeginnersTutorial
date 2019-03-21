using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Beginners.Models
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string EmpFName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string EmpLName { get; set; }
        [Required]
        [DisplayName("Department")]
        public string Department { get; set; }
        [Required]
        [DisplayName("Status")]
        public int EmpStatus { get; set; }
    }
}