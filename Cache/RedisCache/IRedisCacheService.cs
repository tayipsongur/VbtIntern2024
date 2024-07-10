using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache.RedisCache
{
    public interface IRedisCacheService
    {
        void Set(string key, object data);
        void Set(string key, object data, DateTime time);
        bool Remove(string key);
        T Get<T>(string key);
    }
}
