using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
