﻿
using eCommerceTickets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eCommerceTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id) => await _context.Set<T>().Where(a => a.Id == id).FirstOrDefaultAsync();

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(int id, T newEntity)
        {
            // opt 1
            //EntityEntry entityEntry = _context.Entry<T>(newEntity);
            //entityEntry.State = EntityState.Modified;
            
            // opt 2
            //_context.Update<T>(newEntity);

            //opt 3
            _context.Set<T>().Update(newEntity);
            await _context.SaveChangesAsync();
            return newEntity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            // opt 1
            //EntityEntry entityEntry = _context.Entry<T>(entity);
            //entityEntry.State = EntityState.Deleted;

            // opt 2
            //_context.Remove<T>(entity);

            // opt 3
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }
    }
}
