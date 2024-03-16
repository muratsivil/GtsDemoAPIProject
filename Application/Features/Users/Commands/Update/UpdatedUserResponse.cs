using Domain.Enums;

namespace Application.Features.Users.Commands.Update;

public class UpdatedUserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
