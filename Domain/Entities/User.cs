using System.ComponentModel.DataAnnotations;
using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;
public class User : Entity<Guid>
{   
    public required string Name { get; set; }
    public required string Surname { get; set; }
}