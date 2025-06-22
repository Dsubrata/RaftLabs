using RaftLabs.Enterprise.Utility.Enums;
using System.Linq.Expressions;

namespace RaftLabs.Enterprise.WebAPI.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        bool Create(T type);
        bool Create(List<T> types);
        List<T> Get();
        T Get(object Id);
        bool Update(T type, object Id);
        bool Delete(Guid Id);
        bool Delete(List<Guid> Ids);
        List<T> Get(string query, params object[] parameters);
       
    }
}
