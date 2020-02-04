using SearchFight.Shared.Config;
using SearchFight.Shared.Constants;
using SearchFight.Shared.Exceptions;
using SearchFight.Shared.Extensions;
using SearchFight.Engines.Interfaces;
using SearchFight.Engines.Models.Google;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Engines.Clients
{
    public class GoogleEngine : ISearchEngine
    {
        public string Name => "Google";
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly string _googleUrl = ConfigurationAccessor.GoogleUri
            .Replace("{0}", ConfigurationAccessor.GoogleKey)
            .Replace("{1}", ConfigurationAccessor.GoogleCustom);

        public async Task<long> GetResultsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            try
            {
                using (var response = await HttpClient.GetAsync(_googleUrl.Replace("{2}", query)))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new ErrorHandlinghHttpException(Messages.SearchFightHttpException);

                    var result = await response.Content.ReadAsStringAsync();
                    var googleResponse = result.DeserializeJson<Response>();
                    return long.Parse(googleResponse.SearchInformation.TotalResults);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorHandlinghHttpException(ex.Message);
            }
        }
    }
}
