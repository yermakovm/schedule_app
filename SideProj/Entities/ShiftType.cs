using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Entities
{
    public class ShiftType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int DurationHours { get; set; }

    }
}
