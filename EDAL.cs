using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class EDAL:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionstring = ConfigurationManager.AppSettings["sqlConnectionString"];
            optionsBuilder.UseSqlServer(connectionstring);
        }

        public DbSet<EmpMaster> employee { get; set; }
    }
}
