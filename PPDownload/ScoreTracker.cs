using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPDownload.Models;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace PPDownload
{
    internal class ScoreTracker
    {
        private const string _defaultTrackListLocation = @"C:\KHC\PPD\songs\PPDownloaderTracks";

        private CsvParser<InstalledScore> _csvParser =
            new CsvParser<InstalledScore>(
                new CsvParserOptions(true, ','),
                new InstalledScoreMapping()
            );

        private List<InstalledScore> _scoreList;


        internal List<InstalledScore> ScoreList
        {
            get
            {
                if (_scoreList == null)
                {
                    if (!File.Exists(_defaultTrackListLocation))
                    {
                        _scoreList = new List<InstalledScore>();
                        return _scoreList;
                    }

                    try
                    {
                        _scoreList = _csvParser.ReadFromFile(_defaultTrackListLocation, Encoding.UTF8)
                            .Select(x => x.Result).ToList();
                        return _scoreList;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error reading installed songs list");
                        _scoreList = new List<InstalledScore>();
                        return _scoreList;
                    }
                }

                return _scoreList;
            }
        }

        internal bool IsInstalled(LibrarySearchListing listing) => _scoreList.Any(x => x.ScoreID == listing.ScoreID);

        internal async Task AddInstalledTrack(LibrarySearchListing listing, string installPath)
        {
            var score = new InstalledScore()
            {
                ScoreID = listing.ScoreID,
                ScoreName = listing.Title,
                TargetVideoURL = listing.VideoLink,
                DirectoryPath = installPath
            };
            _scoreList.Add(score);
            await SaveInstalledTrackList();
        }

        private async Task SaveInstalledTrackList()
        {
            if (File.Exists(_defaultTrackListLocation))
            {
                File.Delete(_defaultTrackListLocation);
            }
            await File.WriteAllLinesAsync(_defaultTrackListLocation, _scoreList.Select(x => x.ToString()));
        }
    }

    internal class InstalledScore
    {
        /// <summary>
        /// ID of installed song
        /// </summary>
        public string ScoreID { get; set; }

        /// <summary>
        /// Name of installed song
        /// </summary>
        public string ScoreName { get; set; }

        /// <summary>
        /// YouTube link for video score targets
        /// </summary>
        public string TargetVideoURL { get; set; }

        /// <summary>
        /// Installed score directory path
        /// </summary>
        public string DirectoryPath { get; set; }

        public override string ToString() => $"{ScoreID},\"{ScoreName}\",\"{TargetVideoURL}\",\"{DirectoryPath}\"";
    }

    class InstalledScoreMapping : CsvMapping<InstalledScore>
    {
        public InstalledScoreMapping() : base()
        {
            MapProperty(0, x => x.ScoreID);
            MapProperty(1, x => x.ScoreName);
            MapProperty(2, x => x.TargetVideoURL);
            MapProperty(3, x => x.DirectoryPath);
        }
    }
}