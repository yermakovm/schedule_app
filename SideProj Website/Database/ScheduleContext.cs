using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Entity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ScheduleApp.Database
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {

        }

        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
