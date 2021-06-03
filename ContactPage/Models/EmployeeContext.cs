﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactPage.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options){  }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<EmployeeDebt> EmployeesDebts { get; set; }
    }
}
