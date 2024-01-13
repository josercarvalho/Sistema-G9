using SistemaG9.Domain.Interfaces.Aplication;
using SistemaG9.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace SistemaG9.Aplication.Services
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _appBase;
        
        public AppServiceBase(IServiceBase<TEntity> appBase)
        {
            _appBase = appBase;
        }

        public void Add(TEntity obj)
        {
            _appBase.Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _appBase.GetAll();
        }

        public TEntity GetById(int id)
        {
            return _appBase.GetById(id);
        }

        public void Remove(TEntity obj)
        {
            _appBase.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _appBase.Update(obj);
        }

        public void Dispose()
        {
            _appBase.Dispose();
        }
    }
}
