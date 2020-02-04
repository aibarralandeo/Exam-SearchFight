using SearchFight.Shared.Config;
using SearchFight.Shared.Constants;
using SearchFight.Shared.Exceptions;
using SearchFight.Shared.Extensions;
using SearchFight.Engines.Interfaces;
using SearchFight.Engines.Models.Bing;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Engines.Clients
{
    public class BingEngine : ISearchEngine
    {
        public string Name => "Bing";
        private static readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri(ConfigurationAccessor.BingUri),
            DefaultRequestHeaders = { { ConfigurationAccessor.BingSubscriptionTag, ConfigurationAccessor.BingKey } }
        };

        public async Task<long> GetResultsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            try
            {
                using (var response = await HttpClient.GetAsync($"?q={query}"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new ErrorHandlinghHttpException(Messages.SearchFightHttpException);

                    var result = await response.Content.ReadAsStringAsync();
                    var bingResponse = result.DeserializeJson<Response>();
                    return long.Parse(bingResponse.WebPages.TotalEstimatedMatches);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorHandlinghHttpException(ex.Message);
            }
        }
    }
}
