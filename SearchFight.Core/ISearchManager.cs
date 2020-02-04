using SearchFight.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Core
{
    public interface IEngineManager
    {
        Task<string> GetReport(List<string> querys);
        Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> querys);
        IEnumerable<string> SetWinner(List<SearchResult> searchResults);
        string SetTotalWinner(List<SearchResult> searchResults);
        IEnumerable<string> GetMainResults(List<SearchResult> searchResults);
    }
}
