using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Beginners.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text;

namespace MVC5Beginners.Controllers
{
    public class EmployeeController : Controller
    {
        public const string BaseUri = "http://localhost:64756/";
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            List<EmployeeViewModel> employee = new List<EmployeeViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Employee");


                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);                    
                }
            }
                return View(employee);
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {
           return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<ActionResult> Create_PostAsync()
        {
            EmployeeViewModel employeeView = new EmployeeViewModel();
            TryUpdateModel(employeeView,null,null,new string[] { "EmpId" } );
            if(ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);                    
                    JavaScriptSerializer javaScritpSerializer = new JavaScriptSerializer();
                    var emp = javaScritpSerializer.Serialize(employeeView);
                    StringContent stringContent = new StringContent(emp, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/Employee/SaveSelectedEmployee", stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                      return  RedirectToAction("Index","Employee");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete_Get(int empId)
        {
            IList<EmployeeViewModel> employee = new List<EmployeeViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string SubURI = string.Format("api/Employee/GetSpecificEmp?Id={0}", empId);
                HttpResponseMessage response = await client.GetAsync(SubURI);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
                    return View(employee.FirstOrDefault());
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }

            
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete_PostAsync(int empId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync("api/Employee/DeleteSelectedEmp?emp=" + empId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        //[HttpGet]
        //[ActionName("Put")]
        //public async Task<ActionResult> UpdateEmploye_getAsync()
        //{
        //    EmployeeViewModel employeeVM = new EmployeeViewModel();
        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Clear();
        //        client.BaseAddress = new Uri(BaseUri);
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //        string SubURI = string.Format("api/Employee/GetSpecificEmp?Id={0}", empId);
        //        HttpResponseMessage response = await client.GetAsync(SubURI);

        //        if (response.IsSuccessStatusCode)
        //        {

        //        }
        //        return View();
        //}
    }
}