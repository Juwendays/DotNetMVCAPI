using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MVC.Repositories.Interface
{
    public interface IGeneralRepository<Entity>
       where Entity : class
    {
        List<Entity> GetAll();
        Task<Entity> Get(int id);
        HttpStatusCode Post(Entity entity);
        HttpStatusCode Put(int id, Entity entity);
        HttpStatusCode Delete(int id);


    }
}
