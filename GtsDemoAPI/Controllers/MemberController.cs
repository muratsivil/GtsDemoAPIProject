using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using GtsDemoAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Security.Authentication.BasicAuth.Attributes;

namespace GtsDemoAPI;

[Route("api/[controller]")]
[ApiController]
public class MemberController : BaseController
{
    
    [HttpPost, BasicAuthentication]
    public async Task<IActionResult> Add([FromBody] CreateMemberCommand createUserCommand)
    {
        if (Mediator is null)
            return BadRequest("Mediator is null.");
        CreatedMemberResponse response = await Mediator.Send(createUserCommand);
        return Ok(response);
    }

    [HttpGet("{id}"), BasicAuthentication]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (Mediator is null)
            return BadRequest("Mediator is null.");

        GetByIdMemberResponse response = await Mediator.Send(new GetByIdMemberQuery { Id = id });
        return Ok(response);
    }

    [HttpPut, BasicAuthentication]
    public async Task<IActionResult> Update([FromBody] UpdateMemberCommand updateMemberCommand)
    {
        if (Mediator is null)
            return BadRequest("Mediator is null.");
        UpdatedMemberResponse response = await Mediator.Send(updateMemberCommand);
        return Ok(response);
    }
}
