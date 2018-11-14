using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TungstenRssReader.Clients;
using TungstenRssReader.Contracts;
using TungstenRssReader.DataReaders;

namespace TungstenRssReader.Services
{
    public class RssReaderService : IRssReaderService
    {
        private readonly IRssDataReader rssDataReader;

        public RssReaderService(IRssDataReader rssDataReader)
        {
            this.rssDataReader = rssDataReader;
        }

        public async Task<IEnumerable<NewsItemContract>> GetItems()
        {
            return await rssDataReader.GetItem();
        }
    }
}
