using Microsoft.EntityFrameworkCore;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MVC.Models.Login> Login { get; set; }
        public DbSet<MVC.Models.User> User { get; set; }
        public DbSet<MVC.Models.Token> Token { get; set; }
    }
}
