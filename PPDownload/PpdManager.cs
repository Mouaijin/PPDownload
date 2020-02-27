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
        private readonly PpDownloader   _downloader     = new PpDownloader();
        private readonly LibraryScraper _libraryScraper = new LibraryScraper();
        private readonly ScoreTracker   _tracker        = new ScoreTracker();

        public async Task<List<LibrarySearchListing>> SearchScores(string query)
        {
            var tracks = await _libraryScraper.SearchScores(query);
            return tracks.Select(x =>
                                 {
                                     x.IsInstalled = _tracker.IsInstalled(x);
                                     return x;
                                 }).ToList();
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

        public async Task UninstallScore(LibrarySearchListing listing) => await _tracker.UninstallScore(listing);

        public async Task DownloadSong(LibrarySearchListing listing)
        {
            await _downloader.DownloadUnzipWithVideoDefault(listing);
        }
    }
}