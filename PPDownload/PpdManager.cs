using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PPDownload.Models;
using PPDownload.Scraping;

namespace PPDownload
{
    public class PpdManager
    {
        private PpDownloader _downloader = new PpDownloader();
        private LibraryScraper _libraryScraper = new LibraryScraper();
        private ScoreTracker _tracker = new ScoreTracker();

        public IAsyncEnumerable<LibrarySearchListing> SearchScores(string query)
        {
           var tracks = _libraryScraper.SearchScores(query);
           return  tracks.Select(x =>
           {
                x.IsInstalled = _tracker.IsInstalled(x);
                return x;
           });
        }

        public async Task InstallScore(LibrarySearchListing listing)
        {
            var installResult = await _downloader.DownloadUnzipWithVideoDefault(listing);
            if (installResult.IsFailure)
            {
                return; //todo:return information
            }

            var directory = installResult.Value;
            await _tracker.AddInstalledScore(listing, directory);
        }
        
        public async Task DownloadSong(LibrarySearchListing listing)
        {
            await _downloader.DownloadUnzipWithVideoDefault(listing);
        }
    }
}