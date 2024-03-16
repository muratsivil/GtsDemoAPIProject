using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Features.Members.Commands.Create;

public class CreatedMemberResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Gender Gender { get; set; }
    public DateTime CreatedDate { get; set; }
}
