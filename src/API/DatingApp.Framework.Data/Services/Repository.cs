﻿using DatingApp.Common.Helpers;
using DatingApp.Framework.Data.Context;
using DatingApp.Framework.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Framework.Data.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DataContext _dbContext;
        private readonly ILogger _logger;

        public Repository(DataContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;
            _logger = logger.CreateLogger("Base Repository"); ;

            _logger.LogInformation($"This is from Repository {nameof(logger)}");
        }
        public virtual void AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            try
            {
                _dbContext.Set<TEntity>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public virtual void RemoveAsync(object Id)
        {
            TEntity entity = _dbContext.Set<TEntity>().Find(Id);

            if (entity != null)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool traked, string includeProperties = null)
        {
            if (filter == null)
            {
                throw new ArgumentNullException($"filter must not be null");
            }

            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (!traked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<PagedList<TEntity>> GetAllAsync(QueryParams searchParams, Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await PagedList<TEntity>.CreateAsync(query, searchParams.PageNumber, searchParams.PageSize);
        }

        public virtual void RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Set<TEntity>().Update(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> ExecWithStoreProcedureAsync(string query, params object[] parameters)
        {
            return await _dbContext.Set<TEntity>().FromSqlRaw(query, parameters).ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(object id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
    }
}
