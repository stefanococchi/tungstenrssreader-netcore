using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TungstenRssReader.DataReaders
{
    abstract public class CachedDataReader<T> : ICachedDataReader<T> where T : class
    {
        private readonly object cacheLock = new { };
        private readonly long lifetimeMs;
        private CachedItemStore store { get; set; }
        private Task<T> getDataTask { get; set; }

        protected CachedDataReader(long lifetimeMs)
        {
            this.lifetimeMs = lifetimeMs;
        }

        abstract protected Task<T> GetData();

        public Task<T> GetItem()
        {
            if (store != null)
            {
                var isExpired = store.LifetimeMilliseconds > lifetimeMs;
                if (!isExpired)
                {
                    return Task.FromResult(store.Data);
                }
                store = null;
            }

            if (getDataTask == null)
            {
                lock (cacheLock)
                {
                    if (getDataTask == null)
                    {
                        try
                        {
                            getDataTask = GetData();
                            getDataTask.Wait();
                        }
                        catch (Exception ex)
                        {
                            throw new DataReaderException("Error fetching data. See inner exception for details", ex);
                        }
                        store = new CachedItemStore(getDataTask.Result);
                        getDataTask = null;
                    }
                }
            }

            return Task.FromResult(store.Data);
        }

        private class CachedItemStore
        {
            private readonly Stopwatch sw;

            public CachedItemStore(T data)
            {
                this.sw = new Stopwatch();
                this.sw.Start();
                Data = data;
            }

            public T Data { get; }
            public long LifetimeMilliseconds => sw.ElapsedMilliseconds;
        }
    }
}
