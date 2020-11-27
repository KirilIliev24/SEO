using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchEngine.Model;
using Microsoft.AspNetCore.Routing;
using System.Net.Http;
using HtmlAgilityPack;
using SearchEngine.DataBase;
using System.Text.RegularExpressions;

namespace SearchEngine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchEngineController : ControllerBase
    {
        private readonly ILogger<SearchEngineController> _logger;

        private LinkPositionDataAccess linkPosition = new LinkPositionDataAccess();

        public SearchEngineController(ILogger<SearchEngineController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("getByKeyword/{keyword}")]
        public async Task<ActionResult<IEnumerable<Result>>> GetLinks(string keyword)
        {
            var links = await Task.Run(() => linkPosition.GetLinksByKeyWord(keyword));
            return links;
        }

        [HttpGet] //this is not used for now
        [Route("search/getAllLinks")]
        public async Task<ActionResult<IEnumerable<Result>>> GetAllLinks()
        {
            var links = await Task.Run(() => linkPosition.GetAllLinks());
            return links;
        }

        [HttpGet]
        [Route("getKeywords")]
        public async Task<string[]> GetKeywords()
        {
            var links = await Task.Run(() => linkPosition.getAllKeywords());
            return links;
        }

        [HttpPost]
        [Route("search/addKeyword/{keyword}")]
        public async Task AddKeyWord(string keyword)
        {
            await Task.Run(() => linkPosition.AddKeyWord(keyword));
        }

        //this is the old web crawler using HtmlAgilityPack
        [HttpGet]
        [Route("getLinks")]
        public async Task<List<string>> GetLinksFromUrl([FromHeader] string url)
        {
            var listOfLinks = new List<string>();
            var client = new HttpClient();

            try
            {
                var html = await client.GetStringAsync(url);
                if (html == null)
                {
                    listOfLinks.Add("Link could NOT be crawled");
                    return listOfLinks;
                }

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                var links = htmlDoc.DocumentNode.SelectNodes("//a[@href]");


                if (links != null)
                {
                    foreach (var l in links)
                    {
                        string link = l.Attributes["href"].Value;
                        if (link.Contains("http"))
                        {
                            listOfLinks.Add(link);

                        }
                    }
                    return listOfLinks;
                }
                else
                {
                    listOfLinks.Add("Link list was empty");
                    return listOfLinks;
                }
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is HttpRequestException)
                    Console.WriteLine(e.Message);
            }
            listOfLinks.Add("Link could NOT be crawled");
            return listOfLinks;
        }

        //new web crawler using Regex
        [HttpGet]
        [Route("getLinksWithRegex")]
        public async Task<List<string>> CrawlLinks([FromHeader] string url, [FromHeader] string urlTwo, [FromHeader] DateTime date)
        {
            var externalLinks = await Task.Run(() => linkPosition.getExternalLinks(url, urlTwo, date)); 
            return externalLinks;
        }

        [HttpGet]
        [Route("positions/byDatePeriod")]
        public async Task<List<DateAndPosition>> GetLinkPositionsByDatePeriod([FromHeader] string link, [FromHeader] string keywords, [FromHeader] DateTime? startDate, [FromHeader] DateTime? endDate)
        {
            var pos = await Task.Run(() => linkPosition.getLinkPositions(link, keywords).ToList());
            HashSet<DateAndPosition> set = new HashSet<DateAndPosition>();


            if (startDate != null && endDate != null)
            {
                List<DateAndPosition> positions = new List<DateAndPosition>();
                foreach (var p in pos)
                {
                    if (p.Date >= startDate && p.Date <= endDate)
                    {
                        positions.Add(p);
                    }
                }
                return positions;
            }
            else
            {
                return pos;
            }
        }
        //method to get link results by date


        [HttpGet]
        [Route("getMeaningfulText")]
        public async Task<string> GetMeaningfulText([FromHeader] string url, [FromHeader] DateTime date)
        {
            var externalLinks = await Task.Run(() => linkPosition.getMeaningfulText(url, date));
            return externalLinks;
        }

        //method to get external links by date and website

    }
}
