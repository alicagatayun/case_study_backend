using case_study_backend.Model;
using case_study_backend.Model.dto;
using case_study_backend.Result;

namespace case_study_backend.Repository.Interface;

public interface IStoriesRepository : IGenericRepository<Stories>
{
    Task<EndpointResult<List<UserDto>>> GetUserStories(Guid userId);
}