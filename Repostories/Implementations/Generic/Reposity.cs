using Microsoft.EntityFrameworkCore;
using ORMMiniProject.Contexts;
using ORMMiniProject.Models.Common;
using ORMMiniProject.Repostories.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ORMMiniProject.Repostories.Implementations.Generic
{
    public class Reposity<T> : IReposity<T> where T : BaseEntity
    {

        private readonly AppDbContext _context;
        public Reposity()
        {
            _context = new AppDbContext();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {

            _context.Set<T>().Remove(entity);

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _context.Set<T>().Where(predicate).ToListAsync();

            return entities;
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = await query.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

