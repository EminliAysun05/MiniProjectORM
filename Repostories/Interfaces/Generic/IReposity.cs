using ORMMiniProject.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Repostories.Interfaces.Generic
{
    public interface IReposity<T> where T : BaseEntity
    {
        //product
        Task<List<T>> GetAllAsync();
        //Task<T?> GetByIdAsync(int id);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        //Task<List<T>> SearchByNameAsync(string name);

        Task<int> SaveChangesAsync();
        
        Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> predicate);


        //user
    }
}

