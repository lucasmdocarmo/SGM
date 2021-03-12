﻿using Microsoft.EntityFrameworkCore;
using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Shared.Core.Repository.Base
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity,TContext> 
                                                                               where TEntity : BaseEntity 
                                                                               where TContext : DbContext
    {
        protected readonly TContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(TContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IQueryable<TEntity>> GetAllFromQuery()
        {
            return await Task.FromResult(DbSet);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Remove(Guid id)
        {
            var entity = await DbSet.FindAsync(id).ConfigureAwait(true);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await SaveChanges();
            }
        }

        public async Task<bool> SaveChanges()
        {
            Db.ChangeTracker.DetectChanges();

            foreach (var entry in Db.ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("LastUpdated") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("LastUpdated").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdated").CurrentValue = DateTime.Now;
                }
            }

            return await Db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }
}
