using Microsoft.EntityFrameworkCore;
using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Database.Databases
{
    internal class MySqlEntities : ApplicationEntities
    {
        public MySqlEntities(string connectionString) : base(connectionString, DbProvider.MySql)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0));
            _ = optionsBuilder.UseLazyLoadingProxies().UseMySql(connectionString, serverVersion);
        }
    }
}
