using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using TungstenRssReader.Contracts;

namespace TungstenRssReader.Clients
{
    public class BBCRssClient : IRssClient
    {
        private const string FeedUrl = "http://feeds.bbci.co.uk/news/uk/rss.xml";
        private readonly XmlNamespaceManager xmlNsManager;

        public BBCRssClient()
        {
            this.xmlNsManager = new XmlNamespaceManager(new NameTable());
            xmlNsManager.AddNamespace("media", "http://search.yahoo.com/mrss/");
        }
        
        public async Task<IEnumerable<NewsItemContract>> GetItems()
        {
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(FeedUrl);
                switch (res.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var bytes = await res.Content.ReadAsByteArrayAsync();
                        using (var stream = new MemoryStream(bytes))
                        {
                            var doc = new XPathDocument(stream);
                            var nav = doc.CreateNavigator();
                            var iterator = nav.Select("/rss/channel/item");
                            var result = new List<NewsItemContract>();
                            while (iterator.MoveNext())
                            {
                                var newsItem = ParseItemNode(iterator.Current);
                                result.Add(newsItem);
                            }
                            return result;
                        }
                    default:
                        throw new NotImplementedException($"Unexpected http status code (expected: {HttpStatusCode.OK}; actual: {res.StatusCode})");
                }
            }
        }

        private NewsItemContract ParseItemNode(XPathNavigator node)
        {
            var result = new NewsItemContract
            {
                title = node.SelectSingleNode("title")?.Value,
                description = node.SelectSingleNode("description")?.Value,
                link = node.SelectSingleNode("link")?.Value,
                guid = node.SelectSingleNode("guid")?.Value,
                pubDate = node.SelectSingleNode("pubDate")?.Value
            };

            var thumbnailNode = node.SelectSingleNode("media:thumbnail", xmlNsManager);
            if (thumbnailNode != null)
            {
                var stringHeight = thumbnailNode.SelectSingleNode("@height")?.Value;
                var stringWidth = thumbnailNode.SelectSingleNode("@width")?.Value;
                var url = thumbnailNode.SelectSingleNode("@url")?.Value;
                if (stringHeight != null && stringWidth != null && url != null)
                {
                    result.thumbnail = new NewsThumbnailContract
                    {
                        height = int.Parse(stringHeight),
                        width = int.Parse(stringWidth),
                        url = url
                    };
                }
            }
            return result;
        }
    }
}
