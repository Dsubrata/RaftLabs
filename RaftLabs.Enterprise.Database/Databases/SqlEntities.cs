using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Database.Databases
{
    internal class SqlEntities : ApplicationEntities
    {
        public SqlEntities(string connectionString) : base(connectionString, DbProvider.Sql)
        {
        }

       
    }
}