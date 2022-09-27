using MVC.Models;
using MVC.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Http;

using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MCC69APP.Controllers
{
    public class HttpAPi<tEntity>
        where tEntity : class, IEntity
    {
        string EndPointString;
        public HttpAPi(string EndPointString)
        {
            this.EndPointString = EndPointString;
        }
        // GET ALL
        string Baseurl = "https://localhost:5001/api/";
        public IEnumerable<tEntity> Get(string token = null)
        {
            IEnumerable<tEntity> entity = null;
            using (var client = new HttpClient())
            {

                if (token != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                //
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET
                var responseTask = client.GetAsync(EndPointString);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JArray data = (JArray)rss["data"];
                    entity = JsonConvert.DeserializeObject<List<tEntity>>(JsonConvert.SerializeObject(data));


                    //RootObject<Regions> ro = JsonConvert.DeserializeObject<RootObject<Regions>>(ResultJsonString.Result);
                    // regions = ro.Data;
                }
                else //web api sent error response 
                {


                    entity = Enumerable.Empty<tEntity>();

                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return entity;

        }
        // GET By ID

        public tEntity Get(int? id)
        {

            tEntity entity = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                //HTTP GET
                var responseTask = client.GetAsync(EndPointString + "/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JObject data = (JObject)rss["data"];
                    entity = JsonConvert.DeserializeObject<tEntity>(JsonConvert.SerializeObject(data));
                }
                else //web api sent error response 
                {
                    entity = null;
                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return entity;


        }
        // PUT
        public string Edit(tEntity entity)
        {
            string dataApi = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + EndPointString + "/" + entity.Id.ToString());

                //HTTP POST
                var putTask = client.PutAsJsonAsync<tEntity>(client.BaseAddress, entity);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);

                    dataApi = rss.SelectToken("result").Value<string>();
                }
            }
            return dataApi;
        }
        // POST

        public string Create(tEntity entity)
        {
            string dataApi = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl + EndPointString);

                //HTTP POST
                var postTask = client.PostAsJsonAsync<tEntity>(EndPointString, entity);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);

                    dataApi = rss.SelectToken("result").Value<string>();
                }
            }

            return dataApi;
        }


        //DELETE

        public string Delete(tEntity entity)
        {
            string dataApi = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(EndPointString + "/" + entity.Id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);

                    dataApi = rss.SelectToken("result").Value<string>();
                }
            }

            return dataApi;
        }
    }
}