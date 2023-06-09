namespace case_study_backend.Model;

public class Relations : CommonModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid UserInRelationshipId { get; set; }
}