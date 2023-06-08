using case_study_backend.Repository.Interface;

namespace case_study_backend;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
}