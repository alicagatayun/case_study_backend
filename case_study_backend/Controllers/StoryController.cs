using case_study_backend.Model;
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
}