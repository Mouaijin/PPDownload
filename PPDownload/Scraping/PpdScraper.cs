using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace PPDownload.Scraping
{
    public class PpdScraper
    {
        protected IConfiguration   _config = Configuration.Default.WithDefaultLoader();
        protected IBrowsingContext _context;
        protected string _baseUrl = "https://projectdxxx.me/search/";
        
        protected PpdScraper()
        {
            _context = BrowsingContext.New(_config);
        }

        protected async Task<IDocument> MakeRequest(string query)
        {
            var fmtQuery = UrlEncode(query);
            var url      = _baseUrl + fmtQuery;
            CancellationTokenSource tokens = new CancellationTokenSource();
            tokens.CancelAfter(10000);
            var document = await _context.OpenAsync(url, tokens.Token);
            tokens.Token.ThrowIfCancellationRequested();
            return document;
        }

        protected string UrlEncode(string s) => WebUtility.UrlEncode(s);
    }
}