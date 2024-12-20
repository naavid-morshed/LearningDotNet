using LearningDotNet.Data;
using LearningDotNet.Dtos;
using LearningDotNet.Entities;
using LearningDotNet.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LearningDotNet.Endpoints;

public static class GamesEndPoints
{
    private const string GetGameEndPointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet(
            "",
            async (GameStoreContext dbContext) =>
                await dbContext
                    .Games.Include(game => game.Genre) // Games will only return GenreId, not Genre objects, that's why include needed
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking() // makes sure framework doesn't keep track of these objects as it is simply a get operation
                    .ToListAsync()
        );

        group
            .MapGet(
                "{id}",
                async (int id, GameStoreContext dbContext) =>
                {
                    // Game? game = dbContext.Games.Find(id);
                    Game? game = await dbContext.Games.FindAsync(id);

                    return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
                }
            )
            .WithName(GetGameEndPointName);

        group.MapPost(
            "",
            async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();
                // game.Genre = dbContext.Genres.Find(newGame.GenreId); this is done by ef itself,
                // this line is only needed if genre object is needed in code block

                dbContext.Games.Add(game);
                // dbContext.Add(game); this works too
                // dbContext.SaveChanges();
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    GetGameEndPointName,
                    new { id = game.Id },
                    game.ToGameDetailsDto()
                );
            }
        );

        group.MapPut(
            "{id}",
            async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);

                if (game is null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(game).CurrentValues.SetValues(updateGameDto.ToEntity(id));
                await dbContext.SaveChangesAsync();

                // return Results.NoContent();
                return Results.CreatedAtRoute(
                    GetGameEndPointName,
                    new { id = id },
                    game.ToGameDetailsDto()
                );
            }
        );

        group.MapDelete(
            "{id}",
            async (int id, GameStoreContext dbContext) =>
                await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync()
        );

        return group;
    }
}
