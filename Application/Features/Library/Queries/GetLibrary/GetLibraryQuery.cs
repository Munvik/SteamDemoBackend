using Application.Common.Interfaces;
using Application.DTOs;
using MediatR;

namespace Application.Features.Library.Queries.GetLibrary;

public record GetLibraryQuery : IRequest<IReadOnlyList<LibraryGameDto>>;

public class GetLibraryQueryHandler : IRequestHandler<GetLibraryQuery, IReadOnlyList<LibraryGameDto>>
{
    private const int FakeUserId = 1;
    private readonly IOwnedGameRepository _ownedGameRepository;

    public GetLibraryQueryHandler(IOwnedGameRepository ownedGameRepository)
    {
        _ownedGameRepository = ownedGameRepository;
    }

    public async Task<IReadOnlyList<LibraryGameDto>> Handle(GetLibraryQuery request, CancellationToken cancellationToken)
    {
        var ownedGames = await _ownedGameRepository.GetByUserIdAsync(FakeUserId, cancellationToken);
        return ownedGames
            .Select(x => new LibraryGameDto(
                x.GameId,
                x.Game?.Title ?? string.Empty,
                x.Game?.Price ?? 0,
                x.Game?.Rating ?? 0,
                x.PurchasedAt,
                x.Status,
                x.Game?.Category?.Name ?? string.Empty,
                x.Game?.ImageUrl ?? string.Empty))
            .ToList();
    }
}
