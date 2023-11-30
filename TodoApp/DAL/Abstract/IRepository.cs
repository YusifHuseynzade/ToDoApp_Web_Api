﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        public TEntity Get(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        void Remove(TEntity entity);
        int Commit();
        Task<int> CommitAsync();
        Task UpdateAsync(TEntity entity);
        IQueryable<TEntity> Include(IQueryable<TEntity> query, params string[] includes);
    }
}
