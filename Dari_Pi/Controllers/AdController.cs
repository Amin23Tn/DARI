using System;
using System.Collections.Generic;

using System.Web.Mvc;
using System.Net.Http;
using Dari_Pi.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.Ajax.Utilities;

namespace Dari_Pi.Controllers
{
    public class AdController : Controller
    {


        public Task<HttpResponseMessage> APIResponse { get; private set; }
        public string BaseAddress { get; private set; }
        public object EntityState { get; private set; }


        // GET: Ad
        public ActionResult Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/retrieve-all-ads").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Ad>>().Result;

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
        public ActionResult Create(Ad aa)
        {
            System.Diagnostics.Debug.WriteLine(aa.image);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Ad>("/add-ad", aa).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");

        }
        public ActionResult Like(Comment com,int id)
        {
          
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Comment>("/add-Like/"+id, com).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.DeleteAsync("http://localhost:8081/deleteAd/" + id).Result;
            return RedirectToAction("Index");

        }
        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            Ad ad = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/getAdById-ad/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Ad>();
                    readTask.Wait();

                    ad = readTask.Result;
                }
            }

            return View(ad);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult Edit(Ad ad , int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Ad>("/modify-ad/"+id, ad);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(ad);
        }
public ActionResult nbcom()
{

    Comment ad = null;

    using (var client = new HttpClient())
    {
        client.BaseAddress = new Uri("http://localhost:8081");
        //HTTP GET
        var responseTask = client.GetAsync("/countComments");
        responseTask.Wait();

        var result = responseTask.Result;
        if (result.IsSuccessStatusCode)
        {
            var readTask = result.Content.ReadAsAsync<Comment>();
            readTask.Wait();

            ad = readTask.Result;
        }
    }

    return View(ad);
}
[HttpGet]
        public ActionResult Details(int id)
        {

            Ad ad = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/getAdById-ad/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Ad>();
                    readTask.Wait();

                    ad = readTask.Result;
                }
            }

            return View(ad);
        }
        public ActionResult List(int id)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("/getAllCommentsByAd/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                
                    ViewBag.result = response.Content.ReadAsAsync<Ad>().Result;

            }
            else
            {
                ViewBag.result = "error";
            }
            return View(ViewBag.result);
        }
        [HttpGet]
        public ActionResult CreateCom()
        {
            return View("CreateCom");
        }
        [HttpPost]
        public ActionResult CreateCom(Comment Com , int id)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<Comment>("/add-comment/"+id, Com).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");

        }

        public ActionResult DeleteCom(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.DeleteAsync("http://localhost:8081/remove-comment/" + id).Result;
            return RedirectToAction("Index");

        }
        public ActionResult EditCom(int id)
        {
            Ad ad = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");
                //HTTP GET
                var responseTask = client.GetAsync("/getAdById-ad/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Ad>();
                    readTask.Wait();

                    ad = readTask.Result;
                }
            }

            return View(ad);
        }

        // POST: Event/Edit/5
        [HttpPost]
        public ActionResult EditCom(Comment Com, int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8081");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Comment>("/modify-ad/" + id, Com);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(Com);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string search)
        {

            /*IEnumerable<Appointment> appointments = null;
            var resultAppointmentsJson = await HttpClient.GetAsync("searchappointment/" + search);*/
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/searchad/" + search).Result;
            if (response.IsSuccessStatusCode)
            {
                // var appointments = await response.Content.ReadAsAsync<IList<Appointment>>();//
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Ad>>().Result;
                return View(ViewBag.result);

            }
            else
            {
                var resultAdsJson1 = await Client.GetAsync("/retrieve-all-ads");


                var ad= await resultAdsJson1.Content.ReadAsAsync<IList<Ad>>();


                return View(ad);
            }
        }

    }
}
