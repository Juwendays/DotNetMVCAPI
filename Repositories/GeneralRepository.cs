using Microsoft.AspNetCore.Http;
using MVC.Repositories.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Repositories
{
    public class GeneralRepository<Entity> : IGeneralRepository<Entity>
          where Entity : class
    {
        private readonly string request;
        private readonly string address;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;

        public GeneralRepository(string request)
        {
            this.address = "https://localhost:44392/api/";
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("Token"));
        }

        public HttpStatusCode Delete(int id)
        {
            var result = httpClient.DeleteAsync(request + id).Result;
            return result.StatusCode;
        }

        public async Task<List<Entity>> Get()
        {
            List<Entity> entities = new List<Entity>();

            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<Entity>>(apiResponse);
            }
            return entities;
        }

        public async Task<Entity> Get(int id)
        {
            Entity entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<Entity>(apiResponse);
            }
            return entity;
        }

        public HttpStatusCode Post(Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address + request, content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode Put(int id, Entity entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync(request + id, content).Result;
            return result.StatusCode;
        }
    }
}
