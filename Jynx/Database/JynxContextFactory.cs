using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Jynx.Database
{
    public class JynxContextFactory : IDesignTimeDbContextFactory<JynxContext>
    {
        public JynxContext CreateDbContext(string[] args)
        {
            var config = new Configuration();
            var optionBuilder = new DbContextOptionsBuilder()
                .UseMySql(config.DbConnection, new MySqlServerVersion(new Version(8, 0, 21)));

            return new JynxContext(optionBuilder.Options);
        }
    }
}
