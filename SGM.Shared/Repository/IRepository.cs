using Microsoft.EntityFrameworkCore;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Shared.Core.Repository
{
    public interface IRepository<TEntity,TContext> : IDisposable where TEntity : BaseEntity 
                                                                 where TContext:DbContext
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<IQueryable<TEntity>> GetAllFromQuery();
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Remove(Guid id);
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<bool> SaveChanges();
    }
}
