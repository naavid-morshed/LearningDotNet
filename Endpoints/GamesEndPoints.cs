using LearningDotNet.Dtos;

namespace LearningDotNet.Endpoints;

public static class GamesEndPoints
{
    const string GetGameEndPointName = "GetGame";

    private static readonly List<GameDto> games = [
        new GameDto(1, "The Witcher 3", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
        new GameDto(2, "Cyberpunk 2077", "Action", 59.99m, new DateOnly(2020, 12, 10)),
        new GameDto(3, "Halo Infinite", "Shooter", 49.99m, new DateOnly(2021, 12, 8)),
        new GameDto(4, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
        new GameDto(5, "Grand Theft Auto V", "Action", 29.99m, new DateOnly(2013, 9, 17))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("", () => games);

        group.MapGet("{id}", (int id) =>
        {
            GameDto? gameDto = games.Find(game => game.Id == id);

            return gameDto is null ? Results.NotFound() : Results.Ok(gameDto);
        })
        .WithName(GetGameEndPointName);

        group.MapPost("", (CreateGameDto newGame) =>
        {
            // if (string.IsNullOrEmpty(newGame.Name)) // another way of validation
            // {
            //     return Results.BadRequest("Name is required");
            // }

            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        group.MapPut("{id}", (int id, UpdateGameDto updateGameDto) =>
        {
            int index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new(
                games[index].Id,
                updateGameDto.Name,
                updateGameDto.Genre,
                updateGameDto.Price,
                updateGameDto.ReleaseDate
            );

            // return Results.NoContent();
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = games[index].Id }, games[index]);
        });

        group.MapDelete("{id}", (int id) => games.RemoveAll(game => game.Id == id));

        return group;
    }

}