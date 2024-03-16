using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateTime CreatedDate { get; set; }
}
