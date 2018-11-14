using System.Collections.Generic;
using System.Threading.Tasks;
using TungstenRssReader.Contracts;

namespace TungstenRssReader.Clients
{
    public interface IRssClient
    {
        Task<IEnumerable<NewsItemContract>> GetItems();
    }
}