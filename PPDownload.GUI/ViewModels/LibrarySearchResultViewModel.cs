using System;
using System.Reactive;
using PPDownload.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PPDownload.GUI.ViewModels
{
    public class LibrarySearchResultViewModel
    {
        private readonly LibrarySearchListing _listing;
        private readonly PpdManager _manager;

        public LibrarySearchResultViewModel(LibrarySearchListing listing, PpdManager manager)
        {
            _listing = listing;
            _manager = manager;
            DownloadSong = ReactiveCommand.Create(() => { _manager.InstallScore(_listing); });
        }

        public string   Title            => _listing.Title;
        public string   Author           => _listing.Author;
        public string   Description      => _listing.Description;
        public DateTime Uploaded         => _listing.Uploaded;
        public string   VideoLink        => _listing.VideoLink;
        public string   DownloadLink     => _listing.DownloadLink;
        public decimal? Easy             => _listing.Easy;
        public decimal? Normal           => _listing.Normal;
        public decimal? Hard             => _listing.Hard;
        public decimal? Extreme          => _listing.Extreme;
        public int?     Downloads        => _listing.Downloads;
        public int?     BPM              => _listing.BPM;
        public decimal? Evaluation       => _listing.Evaluation;
        public int?     Evaluators       => _listing.Evaluators;
        public string   EvaluationString => _listing.EvaluationString;
        public string   Length           => _listing.Length;
        public string   ScoreID          => _listing.ScoreID;
        
        public ReactiveCommand<Unit,Unit> DownloadSong { get; }
    }
}