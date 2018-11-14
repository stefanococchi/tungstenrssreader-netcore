using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TungstenRssReader.Clients;
using TungstenRssReader.Contracts;

namespace TungstenRssReader.DataReaders
{
    public class RssDataReader : CachedDataReader<IEnumerable<NewsItemContract>>, IRssDataReader
    {
        private readonly IRssClient rssClient;

        public RssDataReader(IRssClient rssClient) : base(lifetimeMs:5000)
        {
            this.rssClient = rssClient;
        }

        protected override async Task<IEnumerable<NewsItemContract>> GetData()
        {
            return await rssClient.GetItems();
        }
    }
}
