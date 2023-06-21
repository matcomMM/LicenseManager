using LicenseManager.Domain.Models;
using LicenseManager.Domain.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseManager.EntityFramework.Services
{
    public class DataService<T> : IDataService<T> where T : DomainObject
    {
        public readonly LicenseManagerDbContextFactory _dbContextFactory;

        public DataService(LicenseManagerDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        #region Exist
        public virtual bool Exist()
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                return context.Set<T>().Any();
            }
        }
        public virtual bool Exist(Guid id)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                return context.Set<T>().Any(e => e.Id == id);
            }
        }
        public virtual async Task<bool> ExistAsync()
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<T>().AnyAsync();
            }
        }
        public virtual async Task<bool> ExistAsync(Guid id)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<T>().AnyAsync(e => e.Id == id);
            }
        }
        #endregion
        #region Create
        public virtual T Create(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = context.Set<T>().Add(entity);
                context.SaveChanges();

                return createdResult.Entity;
            }
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }
        public virtual bool Create(IEnumerable<T> entities)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (T entity in entities)
                {
                    context.Set<T>().Add(entity);
                }
                context.SaveChanges();

                return true;
            }
        }
        #endregion
        #region Delete
        public virtual bool Delete(Guid id)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                T? _entity = context.Set<T>().FirstOrDefault(e => e.Id == id);
                if (_entity != null)
                {
                    context.Set<T>().Remove(_entity);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public virtual bool Delete(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                if (entity != null)
                {
                    context.Set<T>().Remove(entity);
                    context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public virtual bool Delete(IEnumerable<T> entities)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (T entity in entities)
                {
                    T? e = context.Set<T>().FirstOrDefault(e => e.Id == entity.Id);
                    if (e != null)
                        context.Set<T>().Remove(e);
                }
                context.SaveChanges();

                return true;
            }
        }
        #endregion
        #region Get
        public virtual T Get(Guid id)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                T? _entity = context.Set<T>().FirstOrDefault(e => e.Id == id);
                return _entity;
            }
        }
        public virtual T Get(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                T? _entity = context.Set<T>().FirstOrDefault(e => e == entity);
                return _entity;
            }
        }
        public virtual IEnumerable<T> GetAll()
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<T> _entities = context.Set<T>().ToList();
                return _entities;
            }
        }
        public virtual async Task<T> GetAsync(Guid id)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                T? _entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
                return _entity;
            }
        }
        public virtual async Task<T> GetAsync(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                T? _entity = await context.Set<T>().FirstOrDefaultAsync(e => e == entity);
                return _entity;
            }
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<T> _entities = await context.Set<T>().ToListAsync();
                return _entities;
            }
        }
        #endregion
        #region Update
        public virtual T Update(T entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var entityDtoProps = typeof(T).GetProperties().Where(p => typeof(DomainObject).IsAssignableFrom(p.PropertyType)).ToList();
                foreach (var prop in entityDtoProps)
                {
                    var trackedEntity = context.Find(prop.PropertyType, (prop.GetValue(entity) as DomainObject)?.Id);
                    prop.SetValue(entity, trackedEntity);
                }

                context.Set<T>().Attach(entity);
                context.Set<T>().Update(entity);
                context.SaveChanges();

                return entity;
            };
        }
        public virtual async Task<T> UpdateAsync(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Attach(entity);
                context.Set<T>().Update(entity);

                await context.SaveChangesAsync();

                return entity;
            };
        }
        public virtual bool Update(IEnumerable<T> entities)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (T entity in entities)
                {
                    context.Set<T>().Attach(entity);
                    context.Set<T>().Update(entity);
                }
                context.SaveChanges();

                return true;
            };
        }
        public virtual async Task<bool> UpdateAsync(IEnumerable<T> entities)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (T entity in entities)
                {
                    context.Set<T>().Attach(entity);
                    context.Set<T>().Update(entity);
                }
                await context.SaveChangesAsync();

                return true;
            };
        }
        #endregion
        #region CreateOrUpdate
        public virtual bool CreateOrUpdate(T entity)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                if (context.Set<T>().Any(e => e.Id == entity.Id))
                {
                    context.EntitiesFinder(entity);
                    context.Set<T>().Update(entity);
                }
                else
                {
                    context.Set<T>().Add(entity);
                }

                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }
        public virtual bool CreateOrUpdate(IEnumerable<T> entities)
        {
            using (DbContext context = _dbContextFactory.CreateDbContext())
            {
                foreach (T entity in entities)
                {
                    if (context.Set<T>().Any(e => e.Id == entity.Id))
                    {
                        context.EntitiesFinder(entity);
                        context.Set<T>().Update(entity);
                    }
                    else
                    {
                        context.Set<T>().Add(entity);
                    }
                }

                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            };
        }
        #endregion

    }
}
