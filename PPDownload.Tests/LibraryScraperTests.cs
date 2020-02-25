using System;
using System.Collections.Generic;
using PPDownload.Models;
using PPDownload.Scraping;
using Xunit;

namespace PPDownload.Tests
{
    public class LibraryScraperTests
    {
        [Fact]
        public async void News39SearchTest()
        {
            LibraryScraper lib = new LibraryScraper();
            var searchScores =  lib.SearchScores("news");
            var searchEnumerator = searchScores.GetAsyncEnumerator();
            await searchEnumerator.MoveNextAsync();
            LibrarySearchListing news39 = searchEnumerator.Current;
            LibrarySearchListing expected = new LibrarySearchListing()
                                            {
                                                Author = "Sakura-san (桜-さん)",
                                                BPM    = 132,
                                                Description =
                                                    "I've been working on this song for two weeks, so I really hope you will enjoy it :)",
                                                DownloadLink =
                                                    "https://projectdxxx.me/score-library/download/id/13c2aaad8377b9890b7b4fb03fbaa4d0",
                                                Downloads  = 362,
                                                Easy       = null,
                                                Evaluation = 4.14m,
                                                Evaluators = 37,
                                                Extreme    = 21.08m,
                                                Hard       = null,
                                                Length     = "3:41",
                                                Normal     = null,
                                                Title      = "News 39 ver桜-さん",
                                                Uploaded   = new DateTime(2018, 7, 17, 1, 5, 42),
                                                VideoLink  = "https://www.youtube.com/watch?v=l69v6SVoE9k"
                                            };
            Assert.Equal(expected.Author, news39.Author);
            Assert.Equal(expected.BPM, news39.BPM);
            Assert.Equal(expected.Description, news39.Description);
            Assert.Equal(expected.DownloadLink, news39.DownloadLink);
            Assert.Equal(expected.Downloads, news39.Downloads);
            Assert.Equal(expected.Easy, news39.Easy);
            Assert.Equal(expected.Evaluation, news39.Evaluation);
            Assert.Equal(expected.Evaluators, news39.Evaluators);
            Assert.Equal(expected.Extreme, news39.Extreme);
            Assert.Equal(expected.Hard, news39.Hard);
            Assert.Equal(expected.Length, news39.Length);
            Assert.Equal(expected.Normal, news39.Normal);
            Assert.Equal(expected.Title, news39.Title);
            Assert.Equal(expected.Uploaded, news39.Uploaded);
            Assert.Equal(expected.VideoLink, news39.VideoLink);

        }
    }
}