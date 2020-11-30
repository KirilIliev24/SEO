using SearchEngine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SearchEngine.DataBase.Model;
namespace SearchEngine.DataBase
{
    public class LinkPositionDataAccess
    {
        public void AddKeyWord(string keyWords)
        {
            using (var context = new SearchEngineContext())
            {
                try
                {
                    context.Keywords.Add(new Keywords { Keyword = keyWords});
                    context.SaveChanges();
                }
                catch (Exception e) 
                {
                    if (e is DbUpdateException || e is DbUpdateConcurrencyException)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                

            }
        }

        public async Task<List<Result>> GetAllLinks()
        {
            List<Result> results = new List<Result>();
            using (var context = new SearchEngineContext())
            {
                var allKeywords = await Task.Run(() => context.Keywords.Select(k => k).ToList());
                foreach (var word in allKeywords)
                {
                    var resultsFromPos = context.PositonAndDates.Where(r => r.Keywords.Equals(word.Keyword)).ToArray();

                    foreach (var r in resultsFromPos)
                    {
                        var resultFromLinkDet = context.LinkDetails.Where(p => p.Link.Equals(r.Link)).FirstOrDefault();
                        results.Add(new Result()
                        {
                            Title = resultFromLinkDet.Title,
                            Link = r.Link,
                            Snippet = resultFromLinkDet.Snippet,
                            Index = resultFromLinkDet.Index,
                            keyword = word.Keyword
                        });
                    }
                }
               
                return results;
            }
        }

        public List<Result> GetLinksByKeyWord(string keyWords, DateTime startDate, DateTime endDate)
        {
            HashSet<Result> uniqueResults = new HashSet<Result>(new ResultComparer());
            bool done = false;
            using (var context = new SearchEngineContext())
            {
                while (!done)
                {
                    try
                    {
                        var resultsFromPos = context.PositonAndDates.Where(r => r.Keywords.Equals(keyWords)).ToArray();

                        var resultInDatePeriod = resultsFromPos.Where(r => r.Date.Date >= startDate.Date && r.Date.Date <= endDate.Date).ToArray();

                        foreach (var r in resultInDatePeriod)
                        {
                            var resultFromLinkDet = context.LinkDetails.Where(p => p.Link.Equals(r.Link)).FirstOrDefault();
                            uniqueResults.Add(new Result()
                            {
                                Title = resultFromLinkDet.Title,
                                Link = r.Link,
                                Snippet = resultFromLinkDet.Snippet,
                                Index = resultFromLinkDet.Index,
                                keyword = keyWords
                            });
                        }
                        done = true;
                    }
                    catch (Exception e)
                    {
                        if (e is TimeoutException)
                        {
                            System.Threading.Thread.Sleep(60 * 1000);
                            continue;
                        }

                    }
                }
                return uniqueResults.ToList();
            }
        }

        public string[] getAllKeywords()
        {
            using (var context = new SearchEngineContext())
            {
                var result = context.Keywords.Select(r => r.Keyword).ToArray();
                return result;
            }
        }

        public List<DateAndPosition> getLinkPositions(string link, string keywords)
        {
            List<DateAndPosition> list = new List<DateAndPosition>();
            using (var context = new SearchEngineContext())
            {
                var everything = context.PositonAndDates.Where(p => p.Link.Equals(link) && p.Keywords.Equals(keywords)).Select(p => p).ToArray();
                for (int i = 0; i < everything.Length; i++)
                {
                    list.Add(
                        new DateAndPosition
                        {
                            Position = everything[i].Position,
                            Js = everything[i].Js,
                            Css = everything[i].Css,
                            WordCount = everything[i].WordCount,
                            Date = everything[i].Date

                        }
                   );
                }
                return list;
            }
        }

        public List<string> getExternalLinks(string link, string linkTwo, DateTime date)
        {
            List<string> endResult = new List<string>();
            using (var context = new SearchEngineContext())
            {
                if (linkTwo == null || linkTwo.Equals(string.Empty) || linkTwo.Equals(""))
                {
                    int linkId = context.LinkDetails.Where(p => p.Link.Equals(link)).Select(p => p.Id).FirstOrDefault();
                    var extLinkList = context.ExternalLinks.Where(p => p.Id == linkId && p.date.Date == date.Date).Select(p => p.externalLink).ToList();
                    if (extLinkList.Count == 0)
                    {
                        extLinkList.Add("List was empty or link could not be crawled");
                        return extLinkList;
                    }
                    return extLinkList;
                }
                else
                {
                    int linkIdOne = context.LinkDetails.Where(p => p.Link.Equals(link)).Select(p => p.Id).FirstOrDefault();
                    int linkIdTwo = context.LinkDetails.Where(p => p.Link.Equals(linkTwo)).Select(p => p.Id).FirstOrDefault();
                    var extLinkListOne = context.ExternalLinks.Where(p => p.Id == linkIdOne && p.date.Date == date.Date).Select(p => p.externalLink).ToList();
                    var extLinkListTwo = context.ExternalLinks.Where(p => p.Id == linkIdTwo && p.date.Date == date.Date).Select(p => p.externalLink).ToList();

                    if (extLinkListOne.Count == 0)
                    {
                        extLinkListOne.Add("First list was empty or link could not be crawled");
                        return extLinkListOne;
                    }
                    else if (extLinkListTwo.Count == 0)
                    {
                        extLinkListTwo.Add("No links found for second list");
                        return extLinkListTwo;
                    }
                    else
                    {
                        foreach (string s in extLinkListOne)
                        {
                            if (extLinkListTwo.Contains(s))
                            {
                                endResult.Add(s);
                            }
                        }
                    }
                    return endResult;
                }
                
            }
        }

        public string getMeaningfulText(string link, DateTime dateTime)
        {
            using (var context = new SearchEngineContext())
            {
                string text = context.PositonAndDates.Where(p => p.Link.Equals(link) && p.Date.Date == dateTime.Date).Select(p => p.MeaningfulText).FirstOrDefault();
                if (text.Equals(string.Empty) || text == null)
                {
                    return "No text found for that day";
                }
                return text;
            }
        }
    }
}
