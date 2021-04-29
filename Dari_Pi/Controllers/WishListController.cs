using Dari_PI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Dari_PI.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {


            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8081");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/retrieve-all-wishLists").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<WishList>>().Result;

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
        public ActionResult Create(WishList wsh)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8081");
            client.PostAsJsonAsync<WishList>("/add-wishList", wsh).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
            return RedirectToAction("Index");


        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.DeleteAsync("http://localhost:8081/remove-wishList/" + id).Result;
            return RedirectToAction("Index");

        }
    }
}