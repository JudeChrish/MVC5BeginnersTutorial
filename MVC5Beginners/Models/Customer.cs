using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Beginners.Models
{
    public class Customer
    {
        public int CustId { get; set; }
        public string CustName { get; set; }
        public int CustMobileNo { get; set; }
        public string CustAddress1 { get; set; }
        public string CustAddress2 { get; set; }
        public string CustAddress3 { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}