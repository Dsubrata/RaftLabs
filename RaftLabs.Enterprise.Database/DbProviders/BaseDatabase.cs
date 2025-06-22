using RaftLabs.Enterprise.Database.Interfaces;
using RaftLabs.Enterprise.Utility.Enums;
using System.Linq.Expressions;

namespace RaftLabs.Enterprise.Database.DbProviders
{
    internal abstract class BaseDatabase<T> : IDatabase<T> where T : class
    {
        public abstract bool Create(T type);
        public abstract bool Create(List<T> types);
        public abstract Task<bool> CreateAsync(T type);
        public abstract Task<bool> CreateAsync(List<T> types);
        public abstract bool Delete(object Id);
        public abstract Task<bool> DeleteAsync(object Id);
        public abstract T Get(object Id);
        public abstract Task<T> GetAsync(object Id);
        public abstract List<T> Get();
        public abstract Task<List<T>> GetAsync();
        public abstract List<T> Get(string query, params object[] parameters);
        public abstract List<T> Get(Expression<Func<T, bool>> filter);
        public abstract bool Update(T type, object Id);
        public abstract Task<bool> UpdateAsync(T type, object Id);
        public abstract int Count(Expression<Func<T, bool>> filter);
        public abstract Task<int> CountAsync(Expression<Func<T, bool>> filter);
        public abstract List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, int>> orderby, OrderType orderType, int skip, int take);
        public abstract List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, DateTime>> orderby, OrderType orderType, int skip, int take);
        public abstract List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, string>> orderby, OrderType orderType, int skip, int take);
        
    }
}
