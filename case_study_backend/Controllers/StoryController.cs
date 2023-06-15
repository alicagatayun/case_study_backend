using case_study_backend.Model;
using case_study_backend.Model.dto;
using case_study_backend.Result;
using Microsoft.AspNetCore.Mvc;

namespace case_study_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class StoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    //TODO: Can be added later, maybe! 
    //private readonly IAuthService authService;

    public StoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        //this.authService = _authService;
    }

    [HttpGet("get-all-users")]
    public async Task<EndpointResult<List<User>>> GetAll()
    {
        var result = new EndpointResult<List<User>>();
        try
        {
            return await _unitOfWork.Users.GetAllAsync();
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.ErrorMessage = ex.Message;
            result.Data = null;
            return result;
        }
    }

    [HttpGet("get-user-connections-having-story")]
    public async Task<EndpointResult<List<UserDto>>> GetUserConnectionsHavingStory(String userId)
    {
        var result = new EndpointResult<List<UserDto>>();
        try
        {
            return await _unitOfWork.Stories.GetUserStories(Guid.Parse(userId));
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.ErrorMessage = ex.Message;
            result.Data = null;
            return result;
        }
    }

   
    [HttpGet("get-story-detail")]
    public async Task<EndpointResult<StoryDetailDto>> GetStoryDetail(String storyId)
    {
        var result = new EndpointResult<StoryDetailDto>();
        try
        {
            return await _unitOfWork.Stories.GetStoryDetail(Guid.Parse(storyId));
        }
        catch (Exception ex)
        {
            result.HasError = true;
            result.ErrorMessage = ex.Message;
            result.Data = null;
            return result;
        }
    }

    //CURRENT USER ID : Guid.Parse("e7c7a707-8842-48ff-be7d-5b35a284ea93")

    [HttpGet("get-mine-story")]
    public EndpointResult<Stories> GetMineStory()
    {
        return new EndpointResult<Stories>()
        {
            HasError = false,
            Data = new Stories()
            {
                Id = Guid.Parse("9c1b5327-5c29-4415-bd0d-08bec719b8bb"),
                UserId = Guid.Parse("e7c7a707-8842-48ff-be7d-5b35a284ea93")
            }
        };
    }
}