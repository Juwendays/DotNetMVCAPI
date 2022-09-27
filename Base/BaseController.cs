using Microsoft.AspNetCore.Mvc;
using MVC.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Base
{
    public class BaseController<TEntity, TRepository> : Controller
           where TEntity : class
           where TRepository : IGeneralRepository<TEntity>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> Get(int id)
        {
            var result = await repository.Get(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Post(TEntity entity)
        {
            var result = repository.Post(entity);
            return Json(result);
        }

        [HttpPut]
        public JsonResult Put(int id, TEntity entity)
        {
            var result = repository.Put(id, entity);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = repository.Delete(id);
            return Json(result);
        }
    }
}
