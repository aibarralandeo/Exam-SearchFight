using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchFight.Helper
{
    public static class SearchHelper
    {
        public static List<string> ExtractArgs(string argsString)
        {
            return Regex.Matches(argsString, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value).ToList();
        }

        public static List<string> GenerateDefaultValues()
        {
            var defaultValues = new List<string> {".net", "java", "\"java script\""};

            return defaultValues;
        }
    }
}
