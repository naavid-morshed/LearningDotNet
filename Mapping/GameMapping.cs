using LearningDotNet.Dtos;
using LearningDotNet.Entities;

namespace LearningDotNet.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto createGameDto)
    {
        return new()
        {
            Name = createGameDto.Name,
            GenreId = createGameDto.GenreId,
            Price = createGameDto.Price,
            ReleaseDate = createGameDto.ReleaseDate
        };
    }
    
    public static Game ToEntity(this UpdateGameDto updateGameDto, int id)
    {
        return new()
        {
            Id = id,
            Name = updateGameDto.Name,
            GenreId = updateGameDto.GenreId,
            Price = updateGameDto.Price,
            ReleaseDate = updateGameDto.ReleaseDate
        };
    }

    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new(
                game.Id,
                game.Name,
                game.Genre!.Name, // (!) means genre will never be null
                game.Price,
                game.ReleaseDate
            );
    }

    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new(
                game.Id,
                game.Name,
                game.GenreId, // (!) means genre will never be null
                game.Price,
                game.ReleaseDate
            );
    }
}