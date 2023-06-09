using case_study_backend.Repository.Interface;

namespace case_study_backend;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRelationsRepository Relations { get; }
    IStoryDetailRepository StoryDetails { get; }
    IStoriesRepository Stories { get; }
    IStorySeenRepository StorySeens { get; }
}