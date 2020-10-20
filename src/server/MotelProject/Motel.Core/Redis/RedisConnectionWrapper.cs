using Motel.Core.Caching;
using Motel.Core.Configuration;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Motel.Core.Redis
{
    public class RedisConnectionWrapper : IRedisConnectionWrapper, ILocker
    {
        private bool _disposed = false;
        private readonly MotelConfig _config;
        private readonly object _lock = new object();
        private readonly Lazy<string> _connectionString;
        private volatile ConnectionMultiplexer _connection;
        private volatile RedLockFactory _redisLockFactory;

        public RedisConnectionWrapper(MotelConfig config)
        {
            _config  =config;
            _connectionString = new Lazy<string>(GetConnectionString);
            _redisLockFactory = CreateRedisLockFactory();
        }

        #region Utilities
        protected ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                //Connection disconnected. Disposing connection...
                _connection?.Dispose();

                //Creating new instance of Redis Connection
                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }

            return _connection;
        }
        protected RedLockFactory CreateRedisLockFactory()
        {
            var configurationOptions = ConfigurationOptions.Parse(_connectionString.Value);
            var redLockEndPoints = GetEndPoints().Select(endPoint => new RedLockEndPoint
            {
                EndPoint = endPoint,
                Password = configurationOptions.Password,
                Ssl = configurationOptions.Ssl,
                RedisDatabase = configurationOptions.DefaultDatabase,
                ConnectionTimeout = configurationOptions.ConnectTimeout,
                SyncTimeout = configurationOptions.SyncTimeout,
            }).ToList();
            return RedLockFactory.Create(redLockEndPoints);
        }

        protected string GetConnectionString()
        {
            return _config.RedisConnectionString;
        }
        #endregion

        #region Method

        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                //dispose ConnectionMultiplexer
                _connection?.Dispose();

                //dispose RedLock factory
                _redisLockFactory?.Dispose();
            }
            _disposed = true;
        }
        public void FlushDatabase(RedisDatabaseNumber db)
        {
            var endPoints = GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                GetServer(endPoint).FlushDatabase((int)db);
            }
        }
        public bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action)
        {
            //use RedLock library
            using var redisLock = _redisLockFactory.CreateLock(resource, expirationTime);
            //ensure that lock is acquired
            if (!redisLock.IsAcquired)
                return false;

            //perform action
            action();

            return true;
        }
        public IDatabase GetDatabase(int db)
        {
            return GetConnection().GetDatabase(db);
        }
        public EndPoint[] GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }

        public IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

    }
}
