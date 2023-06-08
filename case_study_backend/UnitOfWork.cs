using System.Data;
using case_study_backend.Repository;
using case_study_backend.Repository.Interface;

namespace case_study_backend;

public class UnitOfWork : IUnitOfWork
{
    private bool _disposed;

    private IDbTransaction? _transaction;
    private IDbConnection? _connection;
    private readonly IConfiguration _configuration;

    public UnitOfWork(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private IUserRepository? _users;

    public IUserRepository Users =>
        _users ??= new UserRepository(_configuration);


    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}