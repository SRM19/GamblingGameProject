using Microsoft.EntityFrameworkCore;
using GamblingGame.Models;

namespace GamblingGame.DbContexts;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Player1",
                Password = "Password1"
            },
            new User
            {
                Id=2,
                Username = "Player2",
                Password = "Password2"
            });
        modelBuilder.Entity<Account>().HasData(
            new Account 
            {
                Id = 1,
                UserId = 1,
                Balance = 10000

            },
            new Account
            {
                Id = 2,
                UserId=2,
                Balance = 10000
            }
            );
    }

}
