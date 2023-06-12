using case_study_backend.Repository.Interface;

namespace case_study_backend.Model;

public class User : CommonModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProfilePhotoPath { get; set; }
}