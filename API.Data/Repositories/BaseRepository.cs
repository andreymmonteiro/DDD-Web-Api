using API.Domain.Entities;
using Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext context;
        private DbSet<T> DataSet;
        public BaseRepository(MyContext context)
        {
            this.context = context;
            this.DataSet = context.Set<T>();
        }
        public async Task<bool> ExistAsync(Guid id)
        {
            return await DataSet.AnyAsync(any => any.Id.Equals(id));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await DataSet.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
                if (result == null)
                    return false;
                context.Remove(result);
                await context.SaveChangesAsync();
                
            }
            catch (Exception error) 
            {
                throw error;
            }
            return true;
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if(item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();
                item.CreateAt = DateTime.UtcNow;
                DataSet.Add(item);
                await context.SaveChangesAsync();
            }
            catch(Exception error) 
            {
                throw error;
            }
            return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await DataSet.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id.Equals(id));
            }
            catch (Exception error) 
            {
                throw error;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await DataSet.AsNoTracking().ToListAsync();
            }
            catch (Exception error) 
            {
                throw error;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await DataSet.AsNoTracking().SingleOrDefaultAsync(entity => entity.Id.Equals(item.Id));
                if (result == null)
                    return null;
                item.UpdateAt = DateTime.UtcNow;
                context.Entry(result).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch(Exception error) 
            {
                throw error;
            }
            return item;
        }
    }
}
