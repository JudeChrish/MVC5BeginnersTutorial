using MVC5Beginners.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MVC5Beginners.Controllers
{
    public class CustomerController : Controller
    {
        public string BaseUri = WebConfigurationManager.AppSettings["BaseUri"];
        // GET: Customer
        public ActionResult Index()
        {
            List<CustomerViewModel> employee = new List<CustomerViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = await client.GetAsync("Employee");


                //if (response.IsSuccessStatusCode)
                //{
                   // var data = await response.Content.ReadAsStringAsync();
                   // employee = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
                //}
            }
            return View();
        }
    }
}