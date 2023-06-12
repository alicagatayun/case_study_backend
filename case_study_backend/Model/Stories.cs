namespace case_study_backend.Model;

public class Stories : CommonModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int NumberOfStoryGroup { get; set; }
}