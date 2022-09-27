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
    public class JobHistoryController : Controller
    {
        MyContext myContext;

        public JobHistoryController(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IActionResult Index()
        {
            var data = myContext.JobHistories
                .Include(x=>x.Employee)
                .Include(y=>y.Department)
                .Include(z=>z.Job)
                .ToList();
            return View(data);
        }

        // create job history page
        public IActionResult create()
        {
            var Jobs = myContext.Jobs.ToList();
            var Departments = myContext.Departments.ToList();
            var Employees = myContext.Employees.ToList();
            ViewBag.Departments = new SelectList(Departments, "Id", "Name");
            ViewBag.Employees = new SelectList(Employees, "Id", "FirstName", "LastName");
            ViewBag.Jobs = new SelectList(Jobs, "Id", "Title");
            return View();
        }

        // create operation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(JobHistory jobHistory)
        {
            myContext.JobHistories.Add(jobHistory);
            int result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "JobHistory");
            return View();
        }

        // edit job history page
        public IActionResult Edit(int id)
        {
            var Jobs = myContext.Jobs.ToList();
            var Departments = myContext.Departments.ToList();
            var Employees = myContext.Employees.ToList();
            ViewBag.Departments = new SelectList(Departments, "Id", "Name");
            ViewBag.Employees = new SelectList(Employees, "Id", "FirstName", "LastName");
            ViewBag.Jobs = new SelectList(Jobs, "Id", "Title");
            var jobHistory = myContext.JobHistories
                .Include(x => x.Employee)
                .Include(y => y.Department)
                .Include(z => z.Job)
                .SingleOrDefault(i=>i.Id.Equals(id));
            return View(jobHistory);
        }

        // edit operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobHistory jobHistory)
        {
            myContext.JobHistories.Update(jobHistory);
            int result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "JobHistory");
            return View();
        }

        // delete confirmation page
        public IActionResult Delete(int id)
        {
            var jobHistory = myContext.JobHistories
                .Include(x => x.Employee)
                .Include(y => y.Department)
                .Include(z => z.Job)
                .SingleOrDefault(i => i.Id.Equals(id));
            return View(jobHistory);
        }

        // delete operation 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(JobHistory jobHistory)
        {
            myContext.JobHistories.Remove(jobHistory);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "JobHistory");
            return View();
        }
    }
}
