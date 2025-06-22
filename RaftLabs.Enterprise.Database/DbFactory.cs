using Autofac;
using RaftLabs.Enterprise.Database.DbProviders;
using RaftLabs.Enterprise.Database.Interfaces;
using RaftLabs.Enterprise.Utility.Enums;

namespace RaftLabs.Enterprise.Database
{
    public abstract class DbFactory<T> where T : class
    {
        private static DbProvider dbProvider;
        private static string connectionString;
        private static readonly ContainerBuilder builder = new();
        private static readonly IContainer container;

        static DbFactory()
        {
            _ = builder.RegisterType<SqlDb<T>>().Keyed<IDatabase<T>>(typeof(SqlDb<T>).Name);
            _ = builder.RegisterType<MySqlDb<T>>().Keyed<IDatabase<T>>(typeof(MySqlDb<T>).Name);
            _ = builder.RegisterType<MongoDb<T>>().Keyed<IDatabase<T>>(typeof(MongoDb<T>).Name);
            _ = builder.RegisterType<CosmosDb<T>>().Keyed<IDatabase<T>>(typeof(CosmosDb<T>).Name);
            container = builder.Build();
        }

        private static IDatabase<T> Resolve(string type)
        {
            using ILifetimeScope scope = container.BeginLifetimeScope();
            return scope.ResolveKeyed<IDatabase<T>>(type, new NamedParameter("connectionString", connectionString));
        }

        public static IDatabase<T> Create(string connectionString, DbProvider dbProvider)
        {
            return dbProvider switch
            {
                DbProvider.Sql => Resolve(typeof(SqlDb<T>).Name),
                DbProvider.MySql => Resolve(typeof(MySqlDb<T>).Name),
                DbProvider.MongoDb => Resolve(typeof(MongoDb<T>).Name),
                DbProvider.CosmosDb => Resolve(typeof(CosmosDb<T>).Name),
                DbProvider.None => throw new Exception("Configure DbProvider"),
                _ => null,
            };
        }
    }
}
