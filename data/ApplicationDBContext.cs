using Ass8.Models;
using Microsoft.EntityFrameworkCore;

namespace Ass8
{
    public class ApplicationDBContext : DbContext{
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options ):base(options)
        {}
        public DbSet<Student> Student {get; set;}
        public DbSet<Teacher> Teacher { get; set; }
    }
}