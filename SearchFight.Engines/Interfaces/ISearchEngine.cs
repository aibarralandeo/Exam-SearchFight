using System.Threading.Tasks;

namespace SearchFight.Engines.Interfaces
{
    public interface ISearchEngine
    {
        string Name { get; }
        Task<long> GetResultsAsync(string query);
    }
}
