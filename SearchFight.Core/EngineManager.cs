using SearchFight.Shared.Constants;
using SearchFight.Shared.Exceptions;
using SearchFight.Shared.Extensions;
using SearchFight.Core.Models;
using SearchFight.Engines.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchFight.Core
{
    public class EngineManager : IEngineManager
    {
        private readonly IEnumerable<ISearchEngine> Clients;
        private readonly StringBuilder StringBuilder;

        public EngineManager(IEnumerable<ISearchEngine> searchClients)
        {
            Clients = searchClients;
            StringBuilder = new StringBuilder();
        }

        public async Task<string> GetReport(List<string> querys)
        {
            if (querys == null)
                throw new ArgumentNullException(nameof(querys));
            try
            {
                var searchResults = await GetResultsAsync(querys.Distinct());

                var winners = SetWinner(searchResults);
                var totalWinner = SetTotalWinner(searchResults);
                var mainResults = GetMainResults(searchResults);

                mainResults.ToList().ForEach(queryResults => StringBuilder.AppendLine(queryResults));
                winners.ToList().ForEach(w => StringBuilder.AppendLine(w));

                StringBuilder.AppendLine(totalWinner);

                return StringBuilder.ToString();
            }
            catch (Exception e)
            {
                throw new ErrorFindingResultsException(Messages.SearchErrorFindingResultsException, e);
            }
        }

        public IEnumerable<string> SetWinner(List<SearchResult> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var winners = searchResults
                .OrderBy(result => result.Client)
                .GroupBy(result => result.Client, result => result,
                    (client, result) => new Winner
                    {
                        Client = client,
                        Query = result.MaxValue(r => r.TotalResults).Query
                    })
                .Select(client => $"{client.Client} winner: {client.Query}")
                .ToList();

            return winners;
        }

        public string SetTotalWinner(List<SearchResult> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var totalWinner = searchResults
                .OrderBy(result => result.Client)
                .GroupBy(result => result.Query, result => result,
                    (query, result) => new {Query = query, Total = result.Sum(r => r.TotalResults)})
                .MaxValue(r => r.Total).Query;

            return $"Total winner: {totalWinner}";
        }

        public IEnumerable<string> GetMainResults(List<SearchResult> searchResults)
        {
            if (searchResults == null)
                throw new ArgumentNullException(nameof(searchResults));

            var results = searchResults
                .OrderBy(result => result.Client)
                .ToLookup(result => result.Query, result => result)
                .Select(resultsGroup =>
                        $"{resultsGroup.Key}: {string.Join(" ", resultsGroup.Select(client => $"{client.Client}: {client.TotalResults}"))}")
                    .ToList();

            return results;
        }

        public async Task<List<SearchResult>> GetResultsAsync(IEnumerable<string> querys)
        {
            var results = new List<SearchResult>();

            foreach (var keyword in querys)
            {
                foreach (var searchClient in Clients)
                {
                    results.Add(new SearchResult
                    {
                        Client = searchClient.Name,
                        Query = keyword,
                        TotalResults = await searchClient.GetResultsAsync(keyword)
                    });
                }
            }

            return results;
        }
    }
}
