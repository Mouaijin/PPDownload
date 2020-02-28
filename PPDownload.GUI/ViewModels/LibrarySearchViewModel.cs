using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PPDownload.GUI.ViewModels
{
    public class LibrarySearchViewModel : ReactiveObject
    {
        
        private readonly PpdManager _manager = new PpdManager();
        private string searchText;

        public LibrarySearchViewModel()
        {
            SearchLibrary = ReactiveCommand.CreateFromTask(LoadSearchResultsImpl);
            SearchLibrary.ThrownExceptions.Select(x =>
            {
                Console.WriteLine(x.Message);
                return x;
            });
        }

        [Reactive]
        public string SearchText { get; set; }

        [Reactive]
        public List<LibrarySearchResultViewModel> SearchResults { get; set; }
        
        public ReactiveCommand<Unit, Unit> SearchLibrary { get; }

        private async Task LoadSearchResultsImpl()
        {
            var results = await _manager.SearchScores(SearchText);
            SearchResults = results.Select(x => new LibrarySearchResultViewModel(x, _manager)).ToList();
        }
        
    }
}