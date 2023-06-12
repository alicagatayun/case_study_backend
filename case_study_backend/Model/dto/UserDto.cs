using case_study_backend.Model;

namespace case_study_backend.Model.dto;

public class UserDto
{
    public User user { get; set; }
    public Stories? story { get; set; }
    public bool AllSeen { get; set; }
}