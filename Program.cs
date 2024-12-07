using LearningDotNet.Data;
using LearningDotNet.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connectionString); // this is registered as a scoped service

var app = builder.Build();

app.MapGamesEndpoints();
await app.MigrateDbAsync();

app.Run();
