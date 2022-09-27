using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using MVC.ViewModel;

namespace MVC.Controllers
{
    public class RegionController : Controller
    {

        public async Task<IActionResult> IndexAscyn()
        {
            Json<Region> regionlist = new Json<Region>();
            using (var httpClient = new HttpClient())
            {
                //tempat Token
                string token = HttpContext.Session.GetString("token");
                if (token == null)
                {
                    return View("UnAuthorize");
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync("https://localhost:44374/api/Country"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    regionlist = JsonConvert.DeserializeObject<Json<Region>>(apiResponse);
                }
            }

            return View(regionlist.data);
        }

        // select all data
        public IActionResult Index()
        {
            IEnumerable<Region> regions = null;
            using (var client = new HttpClient())
            {
                string token = HttpContext.Session.GetString("token");
                if (token == null)
                {
                    return View("UnAuthorize");
                }
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                // http get
                var responseTask = client.GetAsync("Region");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JArray data = (JArray)rss["data"];
                    regions = JsonConvert.DeserializeObject<List<Region>>(JsonConvert.SerializeObject(data));
                }
                else
                {
                    regions = Enumerable.Empty<Region>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View(regions);
        }

        // getting create page
        public IActionResult Create()
        {
            return View();
        }

        // create operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Region region)
        {
            using (var client = new HttpClient())
            {
                string token = HttpContext.Session.GetString("token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                var postTask = client.PostAsJsonAsync<Region>("Region", region);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(region);
        }

        // getting edit page
        public IActionResult Edit(int id)
        {
            Region region = null;
            using(var client = new HttpClient())
            {
                string token = HttpContext.Session.GetString("token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                // get by id
                var responseTask = client.GetAsync("Region/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JObject data = (JObject)rss["data"];
                    region = JsonConvert.DeserializeObject<Region>(JsonConvert.SerializeObject(data));
                }
            }
            return View(region);
        }

        // edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Region region)
        {
            using(var client = new HttpClient())
            {
                string token = HttpContext.Session.GetString("token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                var putTask = client.PutAsJsonAsync<Region>("Region/"+region.Id.ToString(), region);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(region);
        }

        // getting delete confirmation page
        public IActionResult Delete(int id)
        {
            Region region = null;
            using (var client = new HttpClient())
            {
                string token = HttpContext.Session.GetString("token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                // get by id
                var responseTask = client.GetAsync("Region/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JObject data = (JObject)rss["data"];
                    region = JsonConvert.DeserializeObject<Region>(JsonConvert.SerializeObject(data));
                }
            }
            return View(region);
        }

        // delete operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Region region)
        {
            using(var client = new HttpClient())
            {
                string token = HttpContext.Session.GetString("token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                var deleteTask = client.DeleteAsync("Region/" + region.Id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
                
            }
            return RedirectToAction("Index");
        }
    }
}
