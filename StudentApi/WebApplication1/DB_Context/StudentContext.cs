using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.DB_Context
{
    public class StudentContext: DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options): base(options)
        {
        }

        public DbSet<Student> StudentTable { get; set; }    
    }
}
