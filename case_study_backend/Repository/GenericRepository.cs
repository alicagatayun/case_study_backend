using System.Transactions;
using case_study_backend.Repository.Interface;
using case_study_backend.Result;
using Dapper;
using Microsoft.Data.SqlClient;

namespace case_study_backend.Repository;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private IConfiguration? Configuration;

    protected abstract string DeleteString { get; }

    protected abstract string InsertString { get; }

    protected abstract string SelectString { get; }
    protected abstract DynamicParameters InsertCommandParametersAdd(T entity);
    protected abstract DynamicParameters UpdateCommandParametersAdd(T entity);

    protected abstract string UpdateString { get; }

    protected GenericRepository(IConfiguration? configuration)
    {
        Configuration = configuration;
    }

    public async Task<EndpointResult<List<T>>> GetAllAsync(string? whereClause = null,
        DynamicParameters? parameters = null)
    {
        var oValidationResult = new EndpointResult<List<T>>();
        try
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);


            var sqlString = SelectString;
            if (whereClause != null)
            {
                sqlString = SelectString + " WHERE " + whereClause;
            }

            if (Configuration != null)
            {
                await using var sqlConnection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));

                await sqlConnection.OpenAsync();
                var model = await sqlConnection.QueryAsync<T>(sqlString, parameters);

                oValidationResult.Data = model.ToList();
                oValidationResult.HasError = false;
            }
            else
            {
                return new EndpointResult<List<T>>()
                {
                    HasError = true,
                    ErrorMessage = "Configuration is NULL",
                    Data = null
                };
            }

            scope.Complete();


            return oValidationResult;
        }
        catch (Exception ex)
        {
            oValidationResult.Data = null;
            oValidationResult.HasError = true;
            oValidationResult.ErrorMessage = ex.Message;
            oValidationResult.ErrorCode = "ERROR CODE : 1000x2252333";
            return oValidationResult;
        }
    }


    public async Task<EndpointResult<T>> AddAsync(T entity)
    {
        var oValidationResult = new EndpointResult<T>();
        var parameters = InsertCommandParametersAdd(entity);
        try
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (Configuration != null)
            {
                await using var sqlConnection =
                    new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                await sqlConnection.OpenAsync();

                await sqlConnection.ExecuteAsync(InsertString, parameters);
            }

            scope.Complete();
            oValidationResult.HasError = false;

            return oValidationResult;
        }
        catch (Exception ex)
        {
            oValidationResult.Data = null;
            oValidationResult.HasError = true;
            oValidationResult.ErrorMessage = ex.Message;
            oValidationResult.ErrorCode = "ERROR CODE : 1000x2252523";
            return oValidationResult;
        }
    }
}