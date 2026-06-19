using Application.Features.Games.Commands.CreateGame;
using Application.Features.Games.Commands.DeleteGame;
using Application.Features.Games.Commands.UpdateGame;
using Application.Features.Games.Queries.GetGameById;
using Application.Features.Games.Queries.GetGames;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery] string? search, [FromQuery] int? categoryId, [FromQuery] string? sortBy, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGamesQuery(search, categoryId, sortBy), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGameById(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetGameByIdQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetGameById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGame(int id, [FromBody] UpdateGameCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command with { Id = id }, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGame(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteGameCommand(id), cancellationToken);
        return NoContent();
    }
}
