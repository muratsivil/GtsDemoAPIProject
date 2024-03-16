using Application.Features.Users.Commands.Create;
using Application.Features.Users.Queries.GetById;
using GtsDemoAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GtsDemoAPI;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
    {
        if (Mediator is null)
            return BadRequest("Mediator is null.");
        CreatedUserResponse response = await Mediator.Send(createUserCommand);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (Mediator is null)
            return BadRequest("Mediator is null.");

        GetByIdUserResponse response = await Mediator.Send(new GetByIdUserQuery { Id = id });
        return Ok(response);
    }
}
