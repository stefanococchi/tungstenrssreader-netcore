using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TungstenRssReader.Clients;
using Xunit;

namespace TungstenRssReader.IntegrationTests.Clients
{
    public class BBCRssClientTests
    {
        private readonly BBCRssClient client;

        public BBCRssClientTests()
        {
            this.client = new BBCRssClient();
        }

        [Fact]
        public async void ShouldGetNews()
        {
            // arrange

            // act
            var news = await client.GetItems();

            // assert
            Assert.NotEmpty(news);
        }
    }
}
