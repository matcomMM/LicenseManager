using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenseManager.Domain.Services
{
    public interface IDataService<T>
    {
        bool Exist();
        bool Exist(Guid id);
        Task<bool> ExistAsync();
        Task<bool> ExistAsync(Guid id);

        T Create(T entity);
        Task<T> CreateAsync(T entity);
        bool Create(IEnumerable<T> entities);

        bool Delete(Guid id);
        bool Delete(T entity);
        bool Delete(IEnumerable<T> entities);

        T Get(Guid id);
        T Get(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();

        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        bool Update(IEnumerable<T> entities);
        Task<bool> UpdateAsync(IEnumerable<T> entities);

        bool CreateOrUpdate(T entity);
        bool CreateOrUpdate(IEnumerable<T> entities);
    }
}
