using System.Collections.Generic;
using System.Threading.Tasks;
using PPDownload.Models;

namespace PPDownload.Scraping
{
    internal class LibraryScraper : PpdScraper
    {
        
        public LibraryScraper() : base()
        {
            _baseUrl += "score?word=";
        }


        public async Task<IEnumerable<LibrarySearchListing>> SearchScores(string query)
        {
            var doc =  await MakeRequest(query);
            var rawListings = doc.Body;
            
            return new List<LibrarySearchListing>();
        }
    }
}