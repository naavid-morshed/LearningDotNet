using LearningDotNet.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("games", () => games);

app.MapGet("games/{id}", (int id) =>
{
    GameDto? gameDto = games.Find(game => game.Id == id);

    return gameDto is null ? Results.NotFound() : Results.Ok(gameDto);
})
.WithName(GetGameEndPointName);

app.MapPost("games", (CreateGameDto newGame) =>
{
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

app.MapPut("games/{id}", (int id, UpdateGameDto updateGameDto) =>
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

app.MapDelete("games/{id}", (int id) => games.RemoveAll(game => game.Id == id));

app.Run();
