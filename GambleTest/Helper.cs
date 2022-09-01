using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GamblingGame;
using GamblingGame.DbContexts;
using GamblingGame.Repository;
using GamblingGame.Service;
using System;

namespace Gamble.UnitTests
{
    public abstract class Helper : IDisposable
    {
        private const string ConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection connection;

        protected GambleService _gambleService;
        public static ILogger<T> GetLogger<T>()
        {
            return LoggerFactory.Create(x => x.AddConsole()).CreateLogger<T>();
        }

        protected Helper()
        {
            connection = new SqliteConnection(ConnectionString);
            _gambleService = GetGambleService();
        }

        protected IGambleRepository GetGambleRepository()
        {
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
            var context = new AppDbContext(options);

            context.Database.EnsureCreated();

            return new GambleRepository(context, mapper);
        }

        protected GambleService GetGambleService()
        {
            return new GambleService(GetGambleRepository(), GetLogger<GambleService>());
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
