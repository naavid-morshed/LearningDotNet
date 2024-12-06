using LearningDotNet.Entities;
using Microsoft.EntityFrameworkCore;

namespace LearningDotNet.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
}