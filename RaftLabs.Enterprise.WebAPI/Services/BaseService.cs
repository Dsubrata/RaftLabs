using RaftLabs.Enterprise.Configuration;
using RaftLabs.Enterprise.Logger;
using RaftLabs.Enterprise.Utility.Enums;
using RaftLabs.Enterprise.WebAPI.Interfaces;
using RaftLabs.Enterprise.WebAPI.Interfaces.Services;
using System.Linq.Expressions;


namespace RaftLabs.Enterprise.WebAPI.Services
{
    internal abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public BaseService(ISettings settings)
        {
            GlobalSettings = settings;
            Repository = new Repository<T>(settings);
            RaftLabs.Enterprise.Logger.Interfaces.ILogger Log = LogFactory.Create(settings);
        }

        protected ILogger Log { get; }
        protected ISettings GlobalSettings;
        protected IRepository<T> Repository { get; }

        bool IBaseService<T>.Create(T type)
        {
            return Repository.Create(type);
        }

        bool IBaseService<T>.Create(List<T> types)
        {
            return Repository.Create(types);
        }

        List<T> IBaseService<T>.Get()
        {
            return Repository.Get();
        }

        bool IBaseService<T>.Update(T type, object Id)
        {
            return Repository.Update(type, Id);
        }

        bool IBaseService<T>.Delete(Guid Id)
        {
            return Repository.Delete(Id);
        }

        bool IBaseService<T>.Delete(List<Guid> Ids)
        {
            return Repository.Delete(Ids);
        }

        List<T> IBaseService<T>.Get(string query, params object[] parameters)
        {
            return Repository.Get(query, parameters);
        }

        T IBaseService<T>.Get(object Id)
        {
            return Repository.Get(Id);
        }
    }
}
