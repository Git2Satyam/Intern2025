using Microsoft.EntityFrameworkCore;
using StudentApp.Core.Config;
using StudentApp.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.DB_Context
{
    public class StudentAppContext: DbContext
    {
        public StudentAppContext(DbContextOptions<StudentAppContext> options): base(options)
        {
        }

        public DbSet<StudentRecord> StudentRecords { get; set; }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentRecordAppConfig());
        }
    }
}
