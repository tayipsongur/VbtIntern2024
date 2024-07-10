using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cache.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly RedisConfigurationService _redisConfigurationService;
        IDatabase _database;
        public RedisCacheService(RedisConfigurationService redisConfigurationService)
        {
            _redisConfigurationService = redisConfigurationService;
            _database = _redisConfigurationService.Database();
        }

        public void Set(string key, object data)
        {
            try
            {
                var dataSerialiaze = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                _redisConfigurationService.Database().StringSet(key, dataSerialiaze);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void Set(string key, object data, DateTime time)
        {
            try
            {
                var dataSerialize = JsonConvert.SerializeObject(data);
                var expiration = time - DateTime.Now;
                _database.StringSet(key, dataSerialize, expiration);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Remove(string key)
        {
            try
            {
                //return _redisConfigurationService.Database().KeyDelete(key);
                return _database.KeyDelete(key);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }
        }

        public T Get<T>(string key) 
        {
            if (key is not null)
            {
                try
                {
                    string jsonData = _redisConfigurationService.Database().StringGet(key);
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message.ToString());
                }
            }
           throw new Exception();

        }
    }
}
