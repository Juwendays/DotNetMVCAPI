using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //public Employee Manager { get; set; }
        //[ForeignKey("Manager")]
        //public int Manager_Id { get; set; } // foreign key
        public Location Location { get; set; }
        [ForeignKey("Location")]
        public int Location_Id { get; set; } // foreign key
    }
}
