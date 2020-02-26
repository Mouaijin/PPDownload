using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PPDownload.Models;
using Xunit;

namespace PPDownload.Tests
{
    public class DownloaderTests : IDisposable
    {
        private readonly LibrarySearchListing _reference = new LibrarySearchListing()
                                                           {
                                                               Author = "Sakura-san (桜-さん)",
                                                               BPM    = 132,
                                                               Description =
                                                                   "I've been working on this song for two weeks, so I really hope you will enjoy it :)",
                                                               DownloadLink =
                                                                   "https://projectdxxx.me/score-library/download/id/13c2aaad8377b9890b7b4fb03fbaa4d0",
                                                               Downloads  = 384,
                                                               Easy       = null,
                                                               Evaluation = 4.14m,
                                                               Evaluators = 37,
                                                               Extreme    = 21.08m,
                                                               Hard       = null,
                                                               Length     = "3:41",
                                                               Normal     = null,
                                                               Title      = "News 39 ver桜-さん",
                                                               Uploaded   = new DateTime(2018, 7, 17, 1, 5, 42),
                                                               VideoLink =
                                                                   "https://www.youtube.com/watch?v=l69v6SVoE9k",
                                                               ScoreID = "13c2aaad8377b9890b7b4fb03fbaa4d0"
        };

        private readonly PpDownloader _ppd = new PpDownloader();
        private const    string       _dir = "test";

        [Fact]
        public async void DownloadTest()
        {
            var result = await _ppd.DownloadZip(_reference, _dir);
            try
            {
                Assert.True(result.IsSuccess);
                var fileInfo = new DirectoryInfo(_dir).GetFiles().FirstOrDefault();
                Assert.NotNull(fileInfo);
                Assert.True(fileInfo.Exists);
                Assert.True(fileInfo.Length > 0);
                Directory.Delete(_dir, true);
            }
            catch (Exception)
            {
                if (Directory.Exists(_dir))
                    Directory.Delete(_dir);
                throw;
            }
        }

        [Fact]
        public async void DownloadUnzipTest()
        {
            var result = await _ppd.DownloadAndUnzip(_reference, _dir);
            try
            {
                Assert.True(result.IsSuccess);
                var dirInfo = new DirectoryInfo(_dir).GetDirectories().FirstOrDefault();
                Assert.NotNull(dirInfo);
                Assert.True(dirInfo.Exists);
                Assert.True(dirInfo.GetFiles().Length > 0);
                Directory.Delete(_dir, true);
            }
            catch (Exception)
            {
                if (Directory.Exists(_dir))
                    Directory.Delete(_dir);
                throw;
            }
        }

        [Fact]
        public async void DownloadUnzipWithVideoTest()
        {
            var result = await _ppd.DownloadUnzipWithVideo(_reference, _dir);
            try
            {
                Assert.True(result.IsSuccess);
                var dirInfo = new DirectoryInfo(_dir).GetDirectories().FirstOrDefault();
                Assert.NotNull(dirInfo);
                Assert.True(dirInfo.Exists);
                Assert.True(dirInfo.GetFiles().Length > 0);
                Assert.Contains(dirInfo.GetFiles(), x => x.Extension == ".mp4");
                Directory.Delete(_dir, true);
            }
            catch (Exception)
            {
                if (Directory.Exists(_dir))
                    Directory.Delete(_dir);
                throw;
            }
        }


        public void Dispose()
        {
            if (Directory.Exists(_dir))
                Directory.Delete(_dir);
        }
    }
}