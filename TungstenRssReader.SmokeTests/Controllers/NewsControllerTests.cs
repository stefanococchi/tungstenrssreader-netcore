using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TungstenRssReader.SmokeTests.Controllers
{
    public class NewsControllerTests : IDisposable
    {
        private readonly string baseUrl;
        private readonly HttpClient httpClient;

        public NewsControllerTests()
        {
            this.baseUrl = Environment.GetEnvironmentVariable("TUNGSTENRSSREADER_LISTENURL") ?? "http://localhost:60942/";
            this.httpClient = new HttpClient();
        }

        public void Dispose()
        {
            httpClient.Dispose();
        }

        [Fact]
        public async void ShouldGetNews()
        {
            // arrange

            // act
            var res = await httpClient.GetAsync(baseUrl + "api/v1/news");

            // assert
            var data = await AssertJsonResponseAsync<IEnumerable<NewsItemContract>>(res);
            Assert.NotEmpty(data);
        }

        private static async Task<T> AssertJsonResponseAsync<T>(HttpResponseMessage msg) where T : class
        {
            Assert.Equal(HttpStatusCode.OK, msg.StatusCode);
            var json = await msg.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(json);
            Assert.NotNull(data);
            return data;
        }
    }
}
