using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ScheduleApp.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}