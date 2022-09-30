using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Repositories.Data
{
    public class LocationRepository : GeneralRepository<Location>
    {
        public LocationRepository(string request = "Location/") : base(request) { 
        
        }
    }
}
