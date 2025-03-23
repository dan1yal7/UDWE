using Microsoft.EntityFrameworkCore;
using UniDwe.Models;

namespace UniDwe.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Registration> registrations { get; set; }

    }
}
