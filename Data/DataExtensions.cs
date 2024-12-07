using Microsoft.EntityFrameworkCore;

namespace LearningDotNet.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
    // public static void MigrateDb(this WebApplication app)
    // {
    //     using var scope = app.Services.CreateScope();
    //     var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    //     dbContext.Database.Migrate();
    // }
}
