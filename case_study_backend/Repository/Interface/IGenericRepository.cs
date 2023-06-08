using case_study_backend.Result;
using Dapper;

namespace case_study_backend.Repository.Interface;

public interface IGenericRepository<T> where T : class
{
    Task<EndpointResult<List<T>>> GetAllAsync(string? whereClause = null, DynamicParameters? parameters = null);
    Task<EndpointResult<T>> AddAsync(T entity);
}