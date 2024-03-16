using System.ComponentModel.DataAnnotations;
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Member : Entity<Guid>
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public Gender Gender { get; set; }
}
