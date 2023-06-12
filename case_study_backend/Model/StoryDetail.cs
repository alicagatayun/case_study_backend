namespace case_study_backend.Model;

public class StoryDetail : CommonModel
{
    public Guid Id { get; set; }
    public Guid StoryId { get; set; }
    public string? ImagePath { get; set; }
    public bool IsVideo { get; set; }
    public int Duration { get; set; }
}