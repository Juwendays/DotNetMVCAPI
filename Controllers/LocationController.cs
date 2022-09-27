using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Context;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class LocationController : Controller
    {
        MyContext myContext;

        public LocationController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        // index page, select all data
        public IActionResult Index()
        {
            //var country = myContext.Countries.Include("Region").ToList();
            var data = myContext.Locations.Include(x => x.Country).ToList();
            return View(data);
        }

        // create location page
        public IActionResult Create()
        {
            var country = myContext.Countries.ToList();
            ViewBag.Countries = new SelectList(country, "Id", "Name"); // getting the countries join data
            return View();
        }

        // create operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            myContext.Locations.Add(location);
            int result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Location");
            return View();
        }

        // edit location page
        public IActionResult Edit(int id)
        {
            var country = myContext.Countries.ToList();
            ViewBag.Countries = new SelectList(country, "Id", "Name");
            var locations = myContext.Locations.Include(x => x.Country).SingleOrDefault(y => y.Id.Equals(id));
            return View(locations);
        }

        // edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Location location)
        {
            myContext.Locations.Update(location);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Location");
            return View();
        }

        // delete location page
        public IActionResult Delete(int id)
        {
            var location = myContext.Locations.Include(x => x.Country).SingleOrDefault(y => y.Id.Equals(id));
            return View(location);
        }

        // delete operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Location location)
        {
            myContext.Locations.Remove(location);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Location");
            return View();
        }
    }
}
