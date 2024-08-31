using Microsoft.EntityFrameworkCore;
using MyDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.DAL.Contexts
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions<MVCDemoDbContext> options):base(options)
        {
            //options will override onConfiguring of DbContext Class it self
            //And i will send the connection string in startup class and inject it here as shown.
        }


        /*
         * Approach 01
        public MVCDemoDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=.;Database=MVCDemoDb;Trusted_Connection=True; TrustServerCertificate=True");

        */

        public DbSet<Department> Departments { get; set; }
    }
}
