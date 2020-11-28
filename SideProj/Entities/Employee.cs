using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsTrainee { get; set; }
        public List<JobType> JobTypes { get; set; }

    }
}
