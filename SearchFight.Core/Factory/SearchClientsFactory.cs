using SearchFight.Engines.Interfaces;
using System;
using System.Linq;

namespace SearchFight.Core.Factory
{
    public static class SearchClientsFactory
    {
        public static IEngineManager CreateSearchClients()
        {
            var clients = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterface(typeof(ISearchEngine).ToString()) != null)
                .Select(t => Activator.CreateInstance(t) as ISearchEngine);

            return new EngineManager(clients);
        }
    }
}
