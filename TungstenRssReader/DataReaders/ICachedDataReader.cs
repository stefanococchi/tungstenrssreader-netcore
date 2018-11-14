using System.Threading.Tasks;

namespace TungstenRssReader.DataReaders
{
    public interface ICachedDataReader<T> where T : class
    {
        Task<T> GetItem();
    }
}