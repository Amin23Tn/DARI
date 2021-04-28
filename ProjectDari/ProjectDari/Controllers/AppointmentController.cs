using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using ProjectDari.Models;
using System.Threading.Tasks;

namespace ProjectDari.Controllers
{
    public class AppointmentController : Controller
    {
        public string BaseAddress { get; private set; }

        // GET: Appointment
        public ActionResult Index()
        {

            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/retrieve-all-Appointment").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Appointment>>().Result;

            }
            else
            {
                ViewBag.result = "error";
            }
            return View(ViewBag.result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Appointment app)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Appointment>("/add-Appointment", app).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");


        }
      
        public ActionResult Edit(int id)
        {
            Appointment app = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/retrieve-appointment/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Appointment>();
                    readTask.Wait();

                    app = readTask.Result;
                }
            }

            return View(app);
        }

       
        [HttpPost]
        public ActionResult Edit(Appointment app, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Appointment>("/modify-Appointment/" + id, app);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(app);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {

            Appointment app = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/retrieve-appointment/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Appointment>();
                    readTask.Wait();

                    app = readTask.Result;
                }
            }

            return View(app);
        }



        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.DeleteAsync("http://localhost:8081/remove-Appointment/" + id).Result;
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<ActionResult> Index(string search)
        {

            /*IEnumerable<Appointment> appointments = null;
            var resultAppointmentsJson = await HttpClient.GetAsync("searchappointment/" + search);*/
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/search/" + search).Result;
            if (response.IsSuccessStatusCode)
            {
                // var appointments = await response.Content.ReadAsAsync<IList<Appointment>>();//
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Appointment>>().Result;
                return View(ViewBag.result);

            }
            else
            {
                var resultAppointmentsJson1 = await Client.GetAsync("/retrieve-all-Appointment");


                var appointments1 = await resultAppointmentsJson1.Content.ReadAsAsync<IList<Appointment>>();


                return View(appointments1);
            }
        }


    }
}
    
