using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TungstenRssReader.Contracts;

namespace TungstenRssReader.DataReaders
{
    public interface IRssDataReader : ICachedDataReader<IEnumerable<NewsItemContract>>
    {
    }
}
