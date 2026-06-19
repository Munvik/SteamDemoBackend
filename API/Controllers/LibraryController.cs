using Application.Features.Library.Commands.UpdateOwnedGameStatus;
using Application.Features.Library.Queries.GetLibrary;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/library")]
public class LibraryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibraryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetLibrary(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetLibraryQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{gameId:int}/status")]
    public async Task<IActionResult> UpdateStatus(int gameId, [FromBody] UpdateLibraryStatusRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateOwnedGameStatusCommand(gameId, request.Status), cancellationToken);
        return NoContent();
    }

    public record UpdateLibraryStatusRequest(GameStatus Status);
}
