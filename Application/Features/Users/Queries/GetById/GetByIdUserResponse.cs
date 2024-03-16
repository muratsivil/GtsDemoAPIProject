using Domain.Enums;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
