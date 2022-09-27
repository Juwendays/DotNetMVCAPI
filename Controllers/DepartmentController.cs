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
    public class DepartmentController : Controller
    {
        MyContext myContext;

        public DepartmentController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        // index page, select all data
        public IActionResult Index()
        {
            var data = myContext.Departments.Include(x=>x.Location).ToList();
            return View(data);
        }

        // create department page
        public IActionResult Create()
        {
            var location = myContext.Locations.ToList();
            ViewBag.Locations = new SelectList(location, "Id", "StreetAddress");
            return View();
        }

        // create operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            myContext.Departments.Add(department);
            int result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Department");
            return View();
        }

        // edit department page 
        public IActionResult Edit(int id)
        {
            var location = myContext.Locations.ToList();
            ViewBag.Locations = new SelectList(location, "Id", "StreetAddress");
            var departments = myContext.Departments.Include(x=>x.Location).SingleOrDefault(y=>y.Id.Equals(id));
            return View(departments);
        }

        // edit operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
            myContext.Departments.Update(department);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Department");
            return View();
        }

        // delete department page
        public IActionResult Delete(int id)
        {
            var department = myContext.Departments.Include(x => x.Location).SingleOrDefault(y => y.Id.Equals(id));
            return View(department);
        }

        // delete operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            myContext.Departments.Remove(department);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Department");
            return View();
        }
    }
}
