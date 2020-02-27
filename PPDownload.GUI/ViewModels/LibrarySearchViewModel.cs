using System.Collections.Generic;
using ReactiveUI.Fody.Helpers;

namespace PPDownload.GUI.ViewModels
{
    public class LibrarySearchViewModel
    {
        
        private PpdManager _manager = new PpdManager();
        public LibrarySearchViewModel()
        {
            
        }
        
        [Reactive]
        public List<LibrarySearchResultViewModel> SearchResults { get; set; }
        
    }
}