using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TungstenRssReader.Contracts;
using TungstenRssReader.DataReaders;
using TungstenRssReader.Services;
using Xunit;

namespace TungstenRssReader.UnitTests.Services
{
    public class RssReaderServiceTests
    {
        private readonly Mock<IRssDataReader> mockRssDataReader;
        private readonly RssReaderService rssReaderService;

        public RssReaderServiceTests()
        {
            this.mockRssDataReader = new Mock<IRssDataReader>(MockBehavior.Strict);
            this.rssReaderService = new RssReaderService(mockRssDataReader.Object);
        }

        [Fact]
        public async void ShouldGetItems()
        {
            // arrange
            IEnumerable<NewsItemContract> items = new NewsItemContract[0];

            mockRssDataReader.Setup(x => x.GetItem()).Returns(Task.FromResult(items)).Verifiable();

            // act
            var actualItems = await rssReaderService.GetItems();

            // assert
            Assert.Same(items, actualItems);
        }
    }
}
