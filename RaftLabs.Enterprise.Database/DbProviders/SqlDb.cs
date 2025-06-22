using Microsoft.EntityFrameworkCore;
using RaftLabs.Enterprise.Database.Databases;
using RaftLabs.Enterprise.Utility.Enums;
using System.Data;
using System.Linq.Expressions;

namespace RaftLabs.Enterprise.Database.DbProviders
{
    internal class SqlDb<T> : BaseDatabase<T> where T : class
    {
        private readonly string connectionString;

        public SqlDb(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override bool Create(T type)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            _ = dbSet.Add(type);
            return dbContext.SaveChanges() > 0;
        }

        public override bool Create(List<T> types)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            dbSet.AddRange(types);
            return dbContext.SaveChanges() > 0;
        }

        public override async Task<bool> CreateAsync(T type)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            _ = dbSet.Add(type);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public override async Task<bool> CreateAsync(List<T> types)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            dbSet.AddRange(types);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public override bool Delete(object id)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            _ = dbSet.Remove(dbSet.Find(id));
            return dbContext.SaveChanges() > 0;
        }

        public override async Task<bool> DeleteAsync(object id)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            _ = dbSet.Remove(dbSet.Find(id));
            return await dbContext.SaveChangesAsync() > 0;
        }

        public override T Get(object id)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return dbSet.Find(id);
        }

        public override async Task<T> GetAsync(object id)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return await dbSet.FindAsync(new object[] { id });
        }

        public override List<T> Get()
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return dbSet.ToList();
        }

        public override async Task<List<T>> GetAsync()
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return await dbSet.ToListAsync();
        }

        public override List<T> Get(string query, params object[] parameters)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return dbSet.ToList();
        }

        public override List<T> Get(Expression<Func<T, bool>> filter)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return dbSet.Where(filter).ToList();
        }

        public override bool Update(T type, object id)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            T entity = dbSet.Find(id);
            dbContext.Entry(entity).CurrentValues.SetValues(type);
            return dbContext.SaveChanges() > 0;
        }

        public override async Task<bool> UpdateAsync(T type, object id)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            T entity = dbSet.Find(id);
            dbContext.Entry(entity).CurrentValues.SetValues(type);
            return await dbContext.SaveChangesAsync() > 0;
        }

        public override int Count(Expression<Func<T, bool>> filter)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return dbSet.Count(filter);
        }

        public override Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return dbSet.CountAsync(filter);
        }

        public override List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, int>> orderby, OrderType orderType, int skip, int take)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return orderType switch
            {
                OrderType.Ascending => dbSet.Where(filter).OrderBy(orderby).Skip(skip).Take(take).ToList(),
                OrderType.Descending => dbSet.Where(filter).OrderByDescending(orderby).Skip(skip).Take(take).ToList(),
                _ => dbSet.Where(filter).OrderBy(orderby).Skip(skip).Take(take).ToList(),
            };
        }

        public override List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, DateTime>> orderby, OrderType orderType, int skip, int take)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return orderType switch
            {
                OrderType.Ascending => dbSet.Where(filter).OrderBy(orderby).Skip(skip).Take(take).ToList(),
                OrderType.Descending => dbSet.Where(filter).OrderByDescending(orderby).Skip(skip).Take(take).ToList(),
                _ => dbSet.Where(filter).OrderBy(orderby).Skip(skip).Take(take).ToList(),
            };
        }

        public override List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, string>> orderby, OrderType orderType, int skip, int take)
        {
            using DbContext dbContext = new SqlEntities(connectionString);
            DbSet<T> dbSet = dbContext.Set<T>();
            return orderType switch
            {
                OrderType.Ascending => dbSet.Where(filter).OrderBy(orderby).Skip(skip).Take(take).ToList(),
                OrderType.Descending => dbSet.Where(filter).OrderByDescending(orderby).Skip(skip).Take(take).ToList(),
                _ => dbSet.Where(filter).OrderBy(orderby).Skip(skip).Take(take).ToList(),
            };
        }
    }

}
