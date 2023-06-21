using LicenseManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseManager.EntityFramework
{
    public static class DbContextExtensions
    {
        public static T EntitiesFinder<T>(this DbContext context, T item) where T : DomainObject
        {
            var localObj = context.Set<T>().Local.FirstOrDefault(entry => entry.Id.Equals(item?.Id));
            if (localObj != null)
            {
                return localObj;
            }
            else
            {
                try
                {
                    context.Set<T>().Attach(item);
                }
                catch
                {
                    item = context.Set<T>().Find(item?.Id);
                }
                return item;
            }
        }

        public static async Task<T> EntitiesFinderAsync<T>(this DbContext context, T item) where T : DomainObject
        {
            var localObj = context.Set<T>().Local.FirstOrDefault(entry => entry.Id.Equals(item?.Id));
            if (localObj != null)
            {
                return localObj;
            }
            else
            {
                try
                {
                    context.Set<T>().Attach(item);
                }
                catch
                {
                    item = await context.Set<T>().FindAsync(item?.Id);
                }
                return item;
            }
        }

        public static bool RemoveWhere<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            return source.Remove(source.FirstOrDefault(predicate));
        }

        public static void RemoveRangeWhere<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            source.ToList().ForEach(i => source.RemoveWhere(predicate));
        }
    }
}
