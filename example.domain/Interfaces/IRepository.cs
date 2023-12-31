﻿using System.Linq.Expressions;

namespace example.domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateRangeAsync(List<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<int> ExecuteWithStoreProcedureAsync(string query, params object[] parameters);
    }
}
