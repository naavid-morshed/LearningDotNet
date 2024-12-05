using LearningDotNet.Dtos;

namespace LearningDotNet.Endpoints;

public class GamesEndPoints
{
    const string GetGameEndPointName = "GetGame";

    List<GameDto> games = [
        new GameDto(1, "The Witcher 3", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
        new GameDto(2, "Cyberpunk 2077", "Action", 59.99m, new DateOnly(2020, 12, 10)),
        new GameDto(3, "Halo Infinite", "Shooter", 49.99m, new DateOnly(2021, 12, 8)),
        new GameDto(4, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
        new GameDto(5, "Grand Theft Auto V", "Action", 29.99m, new DateOnly(2013, 9, 17))
    ];
}