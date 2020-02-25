using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using CSharpFunctionalExtensions;
using PPDownload.Models;

namespace PPDownload.Scraping
{
    internal class LibraryScraper : PpdScraper
    {
        public LibraryScraper()
        {
            _baseUrl += "score?word=";
        }


        public async IAsyncEnumerable<LibrarySearchListing> SearchScores(string query)
        {
            var doc         = await MakeRequest(query);
            var rawListings = doc.Body.QuerySelectorAll("div.panel");
            foreach (IElement listingElement in rawListings)
            {
                var listingResult = ParseSearchListing(listingElement);
                if (listingResult.IsSuccess)
                {
                    yield return listingResult.Value;
                }
                else
                {
                    Console.WriteLine(listingResult.Error);
                }
            }
        }

        private Result<LibrarySearchListing, string> ParseSearchListing(IElement root)
        {
            var title = root.QuerySelector("div.panel-heading > h3 > div > a")?.InnerHtml.CleanInnerHtml() ?? "";
            if (string.IsNullOrEmpty(title))
            {
                return Result.Failure<LibrarySearchListing, string>("Could not retrieve title.");
            }

            var author = root.QuerySelector("div.panel-heading > div.pull-right > a")?.Text()?.CleanInnerHtml();
            if (string.IsNullOrEmpty(author))
            {
                return Result.Failure<LibrarySearchListing, string>("Could not retrieve author.");
            }

            var description = root.QuerySelector("div.panel-body > div.clearfix")?.InnerHtml.CleanInnerHtml() ?? "";
            if (string.IsNullOrEmpty(description))
            {
                return Result.Failure<LibrarySearchListing, string>("Could not retrieve description.");
            }

            var uploadedStr = root.QuerySelector("div.panel-body > div.pull-right > span")?.InnerHtml.Replace("投稿", "")
                                  .Replace("Uploaded", "").CleanInnerHtml() ?? "";
            if (string.IsNullOrEmpty(uploadedStr))
            {
                return Result.Failure<LibrarySearchListing, string>("Could not retrieve upload date.");
            }

            var vidLink = root.QuerySelector("div.panel-body > div.pull-right > input")?.Attributes?["value"]?.Value ??
                          "";
            if (string.IsNullOrEmpty(vidLink))
            {
                return Result.Failure<LibrarySearchListing, string>("Could not retrieve youtube link.");
            }

            var downloadLink = root.QuerySelector("div.panel-body > div.pull-right > a:nth-child(3)")
                                   ?.Attributes?["href"]?.Value ?? "";
            if (string.IsNullOrEmpty(downloadLink))
            {
                return Result.Failure<LibrarySearchListing, string>("Could not retrieve download link.");
            }

            downloadLink = "https://projectdxxx.me" + downloadLink;

            var difficultyTable = root.QuerySelector("table > tbody > tr:nth-of-type(2)");
            var easyStr = difficultyTable?.QuerySelector("td:nth-of-type(1)")?.InnerHtml.CleanInnerHtml()
                                         .Replace("pt", "") ?? "";
            var normalStr = difficultyTable?.QuerySelector("td:nth-of-type(2)")?.InnerHtml.CleanInnerHtml()
                                           .Replace("pt", "") ?? "";
            var hardStr = difficultyTable?.QuerySelector("td:nth-of-type(3)")?.InnerHtml.CleanInnerHtml()
                                         .Replace("pt", "") ?? "";
            var extremeStr = difficultyTable?.QuerySelector("td:nth-of-type(4)")?.InnerHtml.CleanInnerHtml()
                                            .Replace("pt", "") ?? "";
            var downloadsStr = difficultyTable?.QuerySelector("td:nth-of-type(5)")?.InnerHtml.CleanInnerHtml() ?? "";
            var bpmStr       = difficultyTable?.QuerySelector("td:nth-of-type(6)")?.InnerHtml.CleanInnerHtml() ?? "";
            var evalStr      = difficultyTable?.QuerySelector("td:nth-of-type(7)")?.InnerHtml.CleanInnerHtml() ?? "";
            var length       = difficultyTable?.QuerySelector("td:nth-of-type(8)")?.InnerHtml.CleanInnerHtml() ?? "";

            var evalScoreStr = Regex.Match(evalStr, @"([0-9].[0-9]+)\(\<span").Groups[1].Captures.FirstOrDefault()
                                    ?.Value;
            var evalCountStr = Regex.Match(evalStr, @"span\>([0-9]+)\)").Groups[1].Captures.FirstOrDefault()?.Value;


            var easy      = easyStr.ParseDecimalNullable();
            var normal    = normalStr.ParseDecimalNullable();
            var hard      = hardStr.ParseDecimalNullable();
            var extreme   = extremeStr.ParseDecimalNullable();
            var downloads = downloadsStr.ParseIntNullable();
            var bpm       = bpmStr.ParseIntNullable();
            var evalScore = evalScoreStr.ParseDecimalNullable();
            var evalCount = evalCountStr.ParseIntNullable();

            var uploadDate = DateTime.Parse(uploadedStr);

            var result = new LibrarySearchListing()
                         {
                             Author       = author,
                             BPM          = bpm,
                             Description  = description,
                             DownloadLink = downloadLink,
                             Downloads    = downloads,
                             Easy         = easy,
                             Normal       = normal,
                             Hard         = hard,
                             Extreme      = extreme,
                             Evaluation   = evalScore,
                             Evaluators   = evalCount,
                             Length       = length,
                             Title        = title,
                             Uploaded     = uploadDate,
                             VideoLink    = vidLink
                         };


            return Result.Success<LibrarySearchListing, string>(result);
        }
    }
}