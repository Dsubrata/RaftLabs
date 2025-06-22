using Microsoft.EntityFrameworkCore;
using RaftLabs.Enterprise.Domain.DTOs;
using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Database.Databases
{
    internal abstract class ApplicationEntities : DbContext
    {
        protected readonly string connectionString;
        protected readonly DbProvider dbProvider;
        public ApplicationEntities(string connectionString, DbProvider dbProvider)
        {
            this.connectionString = connectionString;
            this.dbProvider = dbProvider;
        }
        public DbSet<UserDTO> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (dbProvider == DbProvider.Sql)
                _ = optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectionString);
            else if (dbProvider == DbProvider.MySql)
                _ = optionsBuilder.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            else if (dbProvider == DbProvider.CosmosDb)
                _ = optionsBuilder.UseLazyLoadingProxies().UseCosmos("endpoint", "account-key", "database-name");
            else if (dbProvider == DbProvider.MongoDb)
                throw new Exception("MongoDb not configured");
            else
                throw new Exception("DbProvider not configured or supported");
        }
    }
}