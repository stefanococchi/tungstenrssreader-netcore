using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TungstenRssReader.DataReaders;
using TungstenRssReader.Services;

namespace TungstenRssReader.Controllers
{
    [Route("api/v1/news")]
    public class NewsController : Controller
    {
        private readonly IRssReaderService rssReaderService;

        public NewsController(IRssReaderService rssReaderService)
        {
            this.rssReaderService = rssReaderService;
        }

        [HttpGet]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 5)]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var news = await rssReaderService.GetItems();
                return Ok(news);
            }
            catch (DataReaderException)
            {
                Response.Headers.Add("Retry-After", new StringValues("5"));
                return StatusCode(503);
            }
        }
    }
}