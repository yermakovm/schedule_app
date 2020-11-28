using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities
{
    public class Shift
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<Job> Jobs { get; set; }
        public ShiftType ShiftType { get; set; }

    }
}
