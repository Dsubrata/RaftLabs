using RaftLabs.Enterprise.Configuration;
using RaftLabs.Enterprise.Database;
using RaftLabs.Enterprise.Database.Interfaces;
using RaftLabs.Enterprise.Utility.Enums;
using RaftLabs.Enterprise.WebAPI.Interfaces;
using System.Linq.Expressions;

namespace RaftLabs.Enterprise.WebAPI
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDatabase<T> repository;
        public Repository(ISettings settings)
        {
            repository = DbFactory<T>.Create(settings.Configuration.ApplicationDbConnectionString, settings.Configuration.DbProvider);
        }

        int IRepository<T>.Count(Expression<Func<T, bool>> filter)
        {
            return repository.Count(filter);
        }

        Task<int> IRepository<T>.CountAsync(Expression<Func<T, bool>> filter)
        {
            return repository.CountAsync(filter);
        }

        bool IRepository<T>.Create(T type)
        {
            return repository.Create(type);
        }

        bool IRepository<T>.Create(List<T> types)
        {
            return repository.Create(types);
        }

        Task<bool> IRepository<T>.CreateAsync(T type)
        {
            return repository.CreateAsync(type);
        }

        Task<bool> IRepository<T>.CreateAsync(List<T> types)
        {
            return repository.CreateAsync(types);
        }

        bool IRepository<T>.Delete(Guid Id)
        {
            return repository.Delete(Id);
        }

        bool IRepository<T>.Delete(List<Guid> Ids)
        {
            foreach (object Id in Ids)
            {
                _ = repository.Delete(Id);
            }
            return true;
        }

        Task<bool> IRepository<T>.DeleteAsync(object Id)
        {
            return repository.DeleteAsync(Id);
        }

        T IRepository<T>.Get(object Id)
        {
            return repository.Get(Id);
        }

        List<T> IRepository<T>.Get()
        {
            return repository.Get();
        }

        List<T> IRepository<T>.Get(string query, params object[] parameters)
        {
            return repository.Get(query, parameters);
        }

        List<T> IRepository<T>.Get(Expression<Func<T, bool>> filter)
        {
            return repository.Get(filter).ToList();
        }

        Task<T> IRepository<T>.GetAsync(object Id)
        {
            return repository.GetAsync(Id);
        }

        Task<List<T>> IRepository<T>.GetAsync()
        {
            return repository.GetAsync();
        }

        List<T> IRepository<T>.Get(Expression<Func<T, bool>> filter, Expression<Func<T, int>> orderby, OrderType orderType, int skip, int take)
        {
            return repository.Get(filter, orderby, orderType, skip, take);
        }

        List<T> IRepository<T>.Get(Expression<Func<T, bool>> filter, Expression<Func<T, string>> orderby, OrderType orderType, int skip, int take)
        {
            return repository.Get(filter, orderby, orderType, skip, take);
        }

        List<T> IRepository<T>.Get(Expression<Func<T, bool>> filter, Expression<Func<T, DateTime>> orderby, OrderType orderType, int skip, int take)
        {
            return repository.Get(filter, orderby, orderType, skip, take);
        }

        bool IRepository<T>.Update(T type, object Id)
        {
            return repository.Update(type, Id);
        }

        Task<bool> IRepository<T>.UpdateAsync(T type, object Id)
        {
            return repository.UpdateAsync(type, Id);
        }
    }
}
