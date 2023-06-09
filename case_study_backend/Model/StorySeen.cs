namespace case_study_backend.Model;

public class StorySeen : CommonModel
{
    public Guid Id { get; set; }
    public Guid StoryDetailId { get; set; }
    public Guid UserId { get; set; }
}