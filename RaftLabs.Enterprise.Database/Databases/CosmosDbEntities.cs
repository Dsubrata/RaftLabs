using Microsoft.EntityFrameworkCore;
using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Database.Databases
{
    internal class CosmosDbEntities : ApplicationEntities
    {
        public CosmosDbEntities(string connectionString) : base(connectionString, DbProvider.CosmosDb)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseLazyLoadingProxies().UseCosmos("endPoint", "account-key", "database-name");
        }
    }
}