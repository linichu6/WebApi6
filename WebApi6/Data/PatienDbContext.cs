using Microsoft.EntityFrameworkCore;

namespace WebApi6.Data
{
    public class PatienDbContext : DbContext
    {
        public PatienDbContext(DbContextOptions<PatienDbContext> options) : base(options)
        {

        }

        public DbSet<Patien> Patients { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
