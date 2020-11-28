using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities
{
    public class Job
    {
        [Key]
        public Guid Id { get; set; }
        public JobType JobType { get; set; } 
        public int RequiredEmployees{ get; set; }
    }
}
