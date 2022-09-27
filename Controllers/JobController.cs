using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Context;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class JobController : Controller
    {
        MyContext myContext;

        // constructor
        public JobController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // select all data
        public IActionResult Index()
        {
            var jobs = myContext.Jobs.ToList(); // getting all the job data
            return View(jobs);
        }

        // getting create page
        public IActionResult Create()
        {
            return View();
        }

        // create operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Job job)
        {
            myContext.Jobs.Add(job); // adding into regions
            int result = myContext.SaveChanges(); // save 
            if (result > 0)
                return RedirectToAction("Index", "Job"); // redirected into region's index (refresh)
            return View();
        }

        // getting edit page
        public IActionResult Edit(int id)
        {
            // getById
            var data = myContext.Jobs.Find(id);
            return View(data);
        }

        // edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Job job)
        {
            //myContext.Entry(region).State = EntityState.Modified;
            myContext.Jobs.Update(job);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Job"); // redirected into region's index (refresh)
            return View();
        }

        // getting delete confirmation page
        public IActionResult Delete(int id)
        {
            var data = myContext.Jobs.Find(id);
            return View(data);
        }

        // delete operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Job job)
        {
            myContext.Jobs.Remove(job);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Job");
            return View();
        }
    }
}
