using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TungstenRssReader.Clients;
using TungstenRssReader.Contracts;
using TungstenRssReader.DataReaders;
using Xunit;

namespace TungstenRssReader.UnitTests.DataReaders
{
    public class RssDataReaderTests : IDisposable
    {
        private readonly Mock<IRssClient> mockClient;
        private readonly RssDataReader rssDataReader;

        public RssDataReaderTests()
        {
            this.mockClient = new Mock<IRssClient>(MockBehavior.Strict);
            this.rssDataReader = new RssDataReader(mockClient.Object);
        }

        public void Dispose()
        {
            mockClient.Verify();
        }

        [Fact]
        public async void ShouldInvokeClientOnceForMultipleCalls()
        {
            // arrange
            const int ActualCalls = 10;
            IEnumerable<NewsItemContract> items = new NewsItemContract[0];

            mockClient.Setup(x => x.GetItems()).Returns(Task.FromResult(items));

            // act
            var results = await Task.WhenAll(Enumerable.Range(0, ActualCalls).Select(x => rssDataReader.GetItem()));

            // assert
            Assert.NotEmpty(results);
            Assert.Equal(ActualCalls, results.Count());
            Assert.All(results, x => ReferenceEquals(items, x));

            mockClient.Verify(x => x.GetItems(), Times.Once);
        }

        [Fact]
        public async void ShouldThrowDataReaderExceptionOnClientException()
        {
            // arrange
            var ex = new Exception();

            mockClient.Setup(x => x.GetItems()).Throws(ex).Verifiable();

            // act
            var actualEx = await Assert.ThrowsAsync<DataReaderException>(() => rssDataReader.GetItem());
        }
    }
}
