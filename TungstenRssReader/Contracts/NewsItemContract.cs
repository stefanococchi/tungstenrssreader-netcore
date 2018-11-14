using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TungstenRssReader.Contracts
{
    public class NewsItemContract
    {
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string pubDate { get; set; }
        public NewsThumbnailContract thumbnail { get; set; }
    }
}
