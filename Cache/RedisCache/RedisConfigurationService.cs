using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Cache.RedisCache
{
    public class RedisConfigurationService
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string connectionString;
        private int _currentDatabase = 0;

       public RedisConfigurationService(IConfiguration configuration)
        {
            RedisConfiguration(configuration);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabase);
        }

        private void RedisConfiguration(IConfiguration configuration)
        {
            string redisEndPoint = configuration.GetSection("RedisConfiguration:RedisEndPoint").Value;
            string redisPort = configuration.GetSection("RedisConfiguration:RedisPort").Value;
            connectionString = $"{redisEndPoint}:{redisPort}";
        }

        public ConnectionMultiplexer RedisConnectionMultiplexer()
        {
            return _connectionMultiplexer;
        }

        public IDatabase Database()
        {
            return _database;
        }
    }
}
