using Jynx.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
