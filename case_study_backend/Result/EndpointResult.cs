namespace case_study_backend.Result;

public class EndpointResult<T>
{
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }
    public string? SuccessMessage { get; set; }
    public bool HasError { get; set; }
}