using System.Collections.Generic;
using System.Threading.Tasks;
using TungstenRssReader.Contracts;

namespace TungstenRssReader.Services
{
    public interface IRssReaderService
    {
        Task<IEnumerable<NewsItemContract>> GetItems();
    }
}