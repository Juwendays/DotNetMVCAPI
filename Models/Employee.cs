using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public Job Job { get; set; }
        [ForeignKey("Job")]
        public int Job_Id { get; set; } // foreign key
        public Employee Manager { get; set; }
        [ForeignKey("Manager")]
        public int? Manager_Id { get; set; } // foregin key with self reference, the "?" means its nullable
        public Department Department { get; set; }
        [ForeignKey("Department")]
        public int Department_Id { get; set; }
    }
}
