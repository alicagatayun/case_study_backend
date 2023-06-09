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
    private IRelationsRepository? _relations;
    private IStoriesRepository? _stories;
    private IStoryDetailRepository? _storyDetail;
    private IStorySeenRepository? _storySeen;

    public IUserRepository Users =>
        _users ??= new UserRepository(_configuration);


    public IRelationsRepository Relations =>
        _relations ??= new RelationsRepository(_configuration);


    public IStoriesRepository Stories =>
        _stories ??= new StoriesRepository(_configuration);


    public IStoryDetailRepository StoryDetails =>
        _storyDetail ??= new StoryDetailRepository(_configuration);


    public IStorySeenRepository StorySeens =>
        _storySeen ??= new StorySeenRepository(_configuration);


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