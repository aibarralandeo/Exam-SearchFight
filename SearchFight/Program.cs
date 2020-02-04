using SearchFight.Shared.Exceptions;
using SearchFight.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchFight.Core.Factory;
using SearchFight.Shared.Constants;

namespace SearchFight
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                List<string> argsList;

                if (args.Length == 0)
                {
                    Console.WriteLine(Messages.WelcomeMessage);
                    var input = Console.ReadLine();
                    argsList = (string.IsNullOrEmpty(input))
                        ? SearchHelper.GenerateDefaultValues()
                        : SearchHelper.ExtractArgs(input);
                }
                else
                {
                    var argsString = string.Join(" ", args);
                    argsList = SearchHelper.ExtractArgs(argsString);
                }

                var searchManager = SearchClientsFactory.CreateSearchClients();
                var result = await searchManager.GetReport(argsList);

                Console.Clear();
                Console.WriteLine(result);
            }
            catch (GenericException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Messages.SearchErrorFindingResultsException}: {ex.Message}");
            }
            Console.ReadKey();
        }
    }
}
