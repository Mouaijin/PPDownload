using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PPDownload.Models;
using VideoLibrary;

namespace PPDownload
{
    public class PpDownloader
    {
        /// <summary>
        /// Downloads a score zip file to the destination directory
        /// </summary>
        /// <param name="listing">Search result listing</param>
        /// <param name="directory">Directory to unzip to</param>
        /// <returns>Success or an error message</returns>
        public async Task<Result> DownloadZip(LibrarySearchListing listing, string directory)
        {
            try
            {
                WebClient client = new WebClient();
                var       data   = await client.DownloadDataTaskAsync(new Uri(listing.DownloadLink));
                string    filename;
                if (!string.IsNullOrEmpty(client.Headers["Content-Disposition"]))
                {
                    filename = client.ResponseHeaders["Content-Disposition"]
                                     .Substring(client.ResponseHeaders["Content-Disposition"]
                                                      .IndexOf("filename=", StringComparison.Ordinal) + 9)
                                     .Replace("\"", "");
                }
                else
                {
                    filename = $"PPDownload-{DateTime.Now:s}.zip";
                }


                File.WriteAllBytes(Path.Join(directory, filename), data);
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }

        /// <summary>
        /// Downloads the score zip to memory, then unzips into the destination directory
        /// </summary>
        /// <param name="listing">Search result listing</param>
        /// <param name="directory">Directory to unzip to</param>
        /// <returns>The created directory or the error message</returns>
        public async Task<Result<string, string>> DownloadAndUnzip(LibrarySearchListing listing, string directory)
        {
            try
            {
                WebClient client           = new WebClient();
                var       data             = await client.DownloadDataTaskAsync(new Uri(listing.DownloadLink));
                using var stream           = new MemoryStream(data);
                using var archive          = new ZipArchive(stream, ZipArchiveMode.Read);
                var       newDirectoryName = archive.Entries.FirstOrDefault()?.Name;
                if (string.IsNullOrEmpty(newDirectoryName))
                {
                    return Result.Failure<string, string>("Could not resolve new directory name, aborting.");
                }

                archive.ExtractToDirectory(directory, true);
                return Result.Success<string, string>(Path.Join(directory, newDirectoryName));
            }
            catch (Exception e)
            {
                return Result.Failure<string, string>(e.Message);
            }
        }

        /// <summary>
        /// Downloads a score zip to memory, unzips it to the destination folder, and downloads the target video for the score
        /// </summary>
        /// <param name="listing">Search result listing</param>
        /// <param name="directory">Directory to unzip to</param>
        /// <returns>The created directory or an error message</returns>
        public async Task<Result> DownloadUnzipWithVideo(LibrarySearchListing listing, string directory)
        {
            try
            {
                var unzipResult = await DownloadAndUnzip(listing, directory);
                if (unzipResult.IsFailure)
                {
                    return unzipResult;
                }

                var directoryPath = unzipResult.Value;
                var youtube       = YouTube.Default;
                var video         = youtube.GetVideo(listing.VideoLink);
                var videoPath     = Path.Join(directoryPath, video.FullName);
                File.WriteAllBytes(videoPath, video.GetBytes());
                return Result.Success();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
        }
    }
}