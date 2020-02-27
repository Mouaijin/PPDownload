using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PPDownload.GUI.ViewModels
{
    public class LibrarySearchViewModel
    {
        
        private readonly PpdManager _manager = new PpdManager();
        public LibrarySearchViewModel()
        {
            SearchLibrary = ReactiveCommand.CreateFromTask(LoadSearchResultsImpl);
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