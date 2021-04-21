using Jynx.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jynx.Database
{
    public class JynxContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public JynxContext(DbContextOptions options) : base(options)
        { }
    }
}
