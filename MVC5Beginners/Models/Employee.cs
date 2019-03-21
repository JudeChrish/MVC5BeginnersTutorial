using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Beginners.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpFName { get; set; }
        public string EmpLName { get; set; }
        public string Department { get; set; }
        public int EmpStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}