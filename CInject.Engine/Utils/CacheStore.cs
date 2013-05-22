using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CInject.Engine.Utils
{
    /// <summary>
    /// In-memory cache-based object initializer
    /// </summary>
    public static class CacheStore
    {
        /// <summary>
        /// In-memory cache dictionary
        /// </summary>
        private static Dictionary<string, object> _cache;
        private static object _sync;


        /// <summary>
        /// Cache initializer
        /// </summary>
        static CacheStore()
        {
            _cache = new Dictionary<string, object>();
            _sync = new object();
        }

        /// <summary>
        /// Check if an object exists in cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Name of key in cache</param>
        /// <returns>True, if yes; False, otherwise</returns>
        public static bool Exists<T>(string key) where T : class
        {
            Type type = typeof(T);
            lock (_sync)
            {
                return _cache.ContainsKey(type.Name + key);
            }
        }

        /// <summary>
        /// Check if an object exists in cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>True, if yes; False, otherwise</returns>
        public static bool Exists<T>() where T : class
        {
            Type type = typeof(T);
            lock (_sync)
            {
                return _cache.ContainsKey(type.Name);
            }
        }

        /// <summary>
        /// Get an object from cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>Object from cache</returns>
        public static T Get<T>() where T : class
        {
            Type type = typeof(T);
            lock (_sync)
            {
                if (_cache.ContainsKey(type.Name) == false)
                    throw new ApplicationException("An object of the desired type does not exist: " + type.Name);

                lock (_sync)
                {
                    return (T)_cache[type.Name];
                }
            }
        }

        /// <summary>
        /// Get an object from cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Name of key in cache</param>
        /// <returns>Object from cache</returns>
        public static T Get<T>(string key) where T : class
        {
            Type type = typeof(T);
            lock (_sync)
            {
                if (_cache.ContainsKey(key + type.Name) == false)
                    throw new ApplicationException(String.Format("An object with key '{0}' does not exists", key));
                lock (_sync)
                {
                    return (T)_cache[key + type.Name];
                }
            }
        }

        /// <summary>
        /// Create default instance of the object and add it in cache
        /// </summary>
        /// <typeparam name="T">Class whose object is to be created</typeparam>
        /// <returns>Object of the class</returns>
        public static T Create<T>(string key, params object[] constructorParameters) where T : class
        {
            Type type = typeof(T);
            T value = (T)Activator.CreateInstance(type, constructorParameters);
            lock (_sync)
            {
                if (_cache.ContainsKey(key + type.Name))
                    throw new ApplicationException(String.Format("An object with key '{0}' already exists", key));
                lock (_sync)
                {
                    _cache.Add(key + type.Name, value);
                }
            }
            return value;
        }

        /// <summary>
        /// Create default instance of the object and add it in cache
        /// </summary>
        /// <typeparam name="T">Class whose object is to be created</typeparam>
        /// <returns>Object of the class</returns>
        public static T Create<T>(params object[] constructorParameters) where T : class
        {
            Type type = typeof(T);
            T value = (T)Activator.CreateInstance(type, constructorParameters);

            lock (_sync)
            {
                if (_cache.ContainsKey(type.Name))
                    throw new ApplicationException(String.Format("An object of type '{0}' already exists", type.Name));
                lock (_sync)
                {
                    _cache.Add(type.Name, value);
                }
            }

            return value;
        }

        public static void Add<T>(string key, T value)
        {
            Type type = typeof(T);

            if (value.GetType() != type)
                throw new ApplicationException(String.Format("The type of value passed to cache {0} does not match the cache type {1} for key {2}", value.GetType().FullName, type.FullName, key));
            lock (_sync)
            {
                if (_cache.ContainsKey(key + type.Name))
                    throw new ApplicationException(String.Format("An object with key '{0}' already exists", key));
                lock (_sync)
                {
                    _cache.Add(key + type.Name, value);
                }
            }
        }

        /// <summary>
        /// Remove an object type from cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        public static void Remove<T>()
        {
            Type type = typeof(T);

            lock (_sync)
            {
                if (_cache.ContainsKey(type.Name) == false)
                    throw new ApplicationException(String.Format("An object of type '{0}' does not exists in cache", type.Name));
                lock (_sync)
                {
                    _cache.Remove(type.Name);
                }
            }
        }

        /// <summary>
        /// Remove an object stored with a key from cache
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="key">Key of the object</param>
        public static void Remove<T>(string key)
        {
            Type type = typeof(T);

            lock (_sync)
            {
                if (_cache.ContainsKey(key + type.Name) == false)
                    throw new ApplicationException(String.Format("An object with key '{0}' does not exists in cache", key));
                lock (_sync)
                {
                    _cache.Remove(key + type.Name);
                }
            }
        }
    }
}
