using Application.Features.Store.Commands.BuyGame;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/store")]
public class StoreController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoreController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("games/{id:int}/buy")]
    public async Task<IActionResult> BuyGame(int id, CancellationToken cancellationToken)
    {
        var ownedGameId = await _mediator.Send(new BuyGameCommand(id), cancellationToken);
        return Ok(new { ownedGameId });
    }
}
