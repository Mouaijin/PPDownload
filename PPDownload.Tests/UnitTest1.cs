using System;
using PPDownload.Scraping;
using Xunit;

namespace PPDownload.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            LibraryScraper lib = new LibraryScraper();
            var searchScores = await lib.SearchScores("news");
        }
    }
}