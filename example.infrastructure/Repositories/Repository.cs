using example.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace example.infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ExampleDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ExampleDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).CreatedDate = DateTime.UtcNow;
                ((IAuditEntity)entity).UpdatedDate = DateTime.UtcNow;
            }
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                entities.ForEach(x =>
                {
                    ((IAuditEntity)x).CreatedDate = DateTime.UtcNow;
                    ((IAuditEntity)x).UpdatedDate = DateTime.UtcNow;
                });
            }

            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public Task<T> UpdateAsync(T entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                ((IAuditEntity)entity).UpdatedDate = DateTime.UtcNow;
            }
            _dbSet.Update(entity);

            return Task.FromResult(entity);
        }

        public Task<List<T>> UpdateRangeAsync(List<T> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(T)))
            {
                entities.ForEach(x =>
                {
                    ((IAuditEntity)x).UpdatedDate = DateTime.UtcNow;
                });
            }
            _dbSet.UpdateRange(entities);

            return Task.FromResult(entities);
        }

        public Task<bool> DeleteAsync(T entity)
        {
            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(T)))
            {
                ((IDeleteEntity)entity).IsDeleted = true;
                _dbSet.Update(entity);
            }
            else
                _dbSet.Remove(entity);

            return Task.FromResult(true);
        }

        public Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var departments = _context.Departments.Where(x => !x.IsDeleted);
            var result = departments.Select(x => new
            {
                x,
                totalRecord = departments.Count()
            }).AsEnumerable();

            return Task.FromResult(_dbSet.Where(expression));
        }

        public async Task<int> ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(query, parameters);
        }

        //public async Task<T> QueryWithStoreProcedureAsync(string query, params object[] parameters)
        //{
        //    return await _dbContext.Database.sq(query, parameters);
        //}
    }
}
