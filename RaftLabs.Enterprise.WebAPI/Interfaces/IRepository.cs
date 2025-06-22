using RaftLabs.Enterprise.Utility.Enums;
using System.Linq.Expressions;

namespace RaftLabs.Enterprise.WebAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        bool Create(T type);
        bool Create(List<T> types);
        Task<bool> CreateAsync(T type);
        Task<bool> CreateAsync(List<T> types);
        T Get(object Id);
        Task<T> GetAsync(object Id);
        List<T> Get();
        Task<List<T>> GetAsync();
        List<T> Get(string query, params object[] parameters);
        List<T> Get(Expression<Func<T, bool>> filter);
        bool Update(T type, object Id);
        Task<bool> UpdateAsync(T type, object Id);
        bool Delete(Guid Id);
        bool Delete(List<Guid> Ids);
        Task<bool> DeleteAsync(object Id);
        int Count(Expression<Func<T, bool>> filter);
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, int>> orderby, OrderType orderType, int skip, int take);
        List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, string>> orderby, OrderType orderType, int skip, int take);
        List<T> Get(Expression<Func<T, bool>> filter, Expression<Func<T, DateTime>> orderby, OrderType orderType, int skip, int take);
    }
}
