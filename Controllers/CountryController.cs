using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class CountryController : Controller
    {
        MyContext myContext;

        public CountryController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        // index page, select all data
        public IActionResult Index()
        {
            IEnumerable<Country> countries = null;
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
                var responseTask = client.GetAsync("Country");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JArray data = (JArray)rss["data"];
                    countries = JsonConvert.DeserializeObject<List<Country>>(JsonConvert.SerializeObject(data));
                }
                else
                {
                    countries = Enumerable.Empty<Country>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View(countries);
        }

        // create country page 
        public IActionResult Create()
        {
            IEnumerable<Country> countries = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                // http get
                var responseTask = client.GetAsync("Country");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JArray data = (JArray)rss["data"];
                    countries = JsonConvert.DeserializeObject<List<Country>>(JsonConvert.SerializeObject(data));
                }
                else
                {
                    countries = Enumerable.Empty<Country>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            ViewBag.Regions = new SelectList(countries, "Id", "Name"); // getting the regions join data
            return View();
        }

        // create operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Country country)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                var postTask = client.PostAsJsonAsync<Country>("Country", country);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View(country);
        }

        // getting edit page 
        public IActionResult Edit(int id)
        {
            IEnumerable<Region> regions = null;
            using (var client = new HttpClient())
            {
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
            ViewBag.Regions = new SelectList(regions, "Id", "Name");
            Country country = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                // get by id
                var responseTask = client.GetAsync("Country/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JObject data = (JObject)rss["data"];
                    country = JsonConvert.DeserializeObject<Country>(JsonConvert.SerializeObject(data));
                }
            }
            return View(country);
        }

        // edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Country country)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                var putTask = client.PutAsJsonAsync<Country>("Country/" + country.Id.ToString(), country);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(country);
        }

        // getting delete confirmation page
        public IActionResult Delete(int id)
        {
            Country country = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                // get by id
                var responseTask = client.GetAsync("Country/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JObject data = (JObject)rss["data"];
                    country = JsonConvert.DeserializeObject<Country>(JsonConvert.SerializeObject(data));
                }
            }
            return View(country);
        }

        // delete operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Country country)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44364/api/");
                var deleteTask = client.DeleteAsync("Country/" + country.Id.ToString());
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
