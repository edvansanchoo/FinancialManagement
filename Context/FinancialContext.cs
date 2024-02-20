using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManagement.Context
{
    public class FinancialContext : DbContext
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Expenses> Expenses {get; set;}

        Constants constants = new Constants();
        protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlServer(constants.GetConnectionString(constants.DATABASE_NAME));
    }
}