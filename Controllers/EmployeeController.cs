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
    public class EmployeeController : Controller
    {
        MyContext myContext;

        public EmployeeController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var data = myContext.Employees
                .Include(x=>x.Department)
                .Include(y=>y.Manager)
                .Include(z=>z.Job)
                .ToList();
            return View(data);
        }
        // create employee page
        public IActionResult Create()
        {
            var Jobs = myContext.Jobs.ToList();
            var Departments = myContext.Departments.ToList();
            var Managers = myContext.Employees.ToList();
            ViewBag.Departments = new SelectList(Departments, "Id", "Name");
            ViewBag.Managers = new SelectList(Managers, "Id", "FirstName" , "LastName");
            ViewBag.Jobs = new SelectList(Jobs, "Id", "Title");
            return View();
        }

        // create operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            myContext.Employees.Add(employee);
            int result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Employee");
            return View();
        }

        // edit employee page
        public IActionResult Edit(int id)
        {
            var Jobs = myContext.Jobs.ToList();
            var Departments = myContext.Departments.ToList();
            var Managers = myContext.Employees.ToList();
            ViewBag.Departments = new SelectList(Departments, "Id", "Name");
            ViewBag.Managers = new SelectList(Managers, "Id", "FirstName", "LastName");
            ViewBag.Jobs = new SelectList(Jobs, "Id", "Title");
            var employee = myContext.Employees
                        .Include(x=>x.Department)
                        .Include(y=>y.Manager)
                        .Include(z=>z.Job)
                        .SingleOrDefault(i=>i.Id.Equals(id));
            return View(employee);
        }

        // edit operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            myContext.Employees.Update(employee);
            int result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Employee");
            return View();
        }

        // delete page
        public IActionResult Delete(int id)
        {
            var employee = myContext.Employees
                        .Include(x => x.Department)
                        .Include(y => y.Manager)
                        .Include(z => z.Job)
                        .SingleOrDefault(i => i.Id.Equals(id));
            return View(employee);
        }

        // delete operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            myContext.Employees.Remove(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Employee");
            return View();
        }
    }
}
