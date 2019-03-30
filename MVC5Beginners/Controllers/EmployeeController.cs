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
using System.Web.Configuration;

namespace MVC5Beginners.Controllers
{
    public class EmployeeController : Controller
    {
        //public const string BaseUri = "http://localhost:64756/";
        public string BaseUri = WebConfigurationManager.AppSettings["BaseUri"];
        // GET: Employee
        public async Task<ActionResult> Index()
        {
            List<EmployeeViewModel> employee = new List<EmployeeViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("Employee/SakreeyaOkkomaSewakayoEwanna");


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
            TryUpdateModel(employeeView, null, null, new string[] { "EmpId" });
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    JavaScriptSerializer javaScritpSerializer = new JavaScriptSerializer();
                    var emp = javaScritpSerializer.Serialize(employeeView);
                    StringContent stringContent = new StringContent(emp, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("Employee/MemaSewakayawaEthulathKranna", stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Employee");
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
                //string SubURI = string.Format("Employee/GetSpecificEmp?Id={0}", empId);
                  string SubURI = string.Format("Employee/MemaAnkayataAdalaSewakayawaEwanna/{0}", empId);
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
        public async Task<ActionResult> Delete_PostAsync()
        {
            EmployeeViewModel emp = new EmployeeViewModel();
            TryUpdateModel(emp);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                //HttpResponseMessage httpResponseMessage = await client.DeleteAsync("Employee/MemaSewakayawaAkreeyaKaranna?emp=" + empId);
                string SubURI = string.Format("Employee/MemaSewakayawaAkreeyaKaranna/{0}", emp.EmpId);
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(SubURI);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete", new { empId = emp.EmpId });
            }
        }

        [HttpGet]
        [ActionName("UpdateEmploye")]
        public async Task<ActionResult> UpdateEmploye_getAsync(int empId)
        {
            IList<EmployeeViewModel> employeeVM = new List<EmployeeViewModel>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //string SubURI = string.Format("Employee/GetSpecificEmp?Id={0}", empId);
                string SubURI = string.Format("Employee/MemaAnkayataAdalaSewakayawaEwanna/{0}", empId); 
                HttpResponseMessage response = await client.GetAsync(SubURI);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    employeeVM = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(data);
                    return View(employeeVM.FirstOrDefault());
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ActionName("UpdateEmploye")]
        public async Task<ActionResult> UpdateEmployee_post()
        {
            EmployeeViewModel employeeView = new EmployeeViewModel();
            TryUpdateModel(employeeView);
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUri);
                    //string SubURI = string.Format("Employee/GetSpecificEmp?Id={0}", employeeView.EmpId);
                    string SubURI = string.Format("Employee/SewakayawaYawathkaleenaKaranna/{0}", employeeView.EmpId);
                    JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
                    var editEmp = scriptSerializer.Serialize(employeeView);
                    StringContent sContent = new StringContent(editEmp,Encoding.UTF8,"application/json");
                    HttpResponseMessage httpResponse = await client.PutAsync(SubURI, sContent);
                    if(httpResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }                
            }
            return View();
        }
    }
}