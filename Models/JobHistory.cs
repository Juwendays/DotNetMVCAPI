using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class JobHistory
    {
        public Employee Employee { get; set; }
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Job Job { get; set; }
        [ForeignKey("Job")]
        public int Job_Id { get; set; } // foreign key
        public Department Department { get; set; }
        [ForeignKey("Department")]
        public int Department_Id { get; set; } // foreign key 
    }
}
