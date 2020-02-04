using System;

namespace SearchFight.Shared.Exceptions
{
    public class ErrorFindingResultsException : GenericException
    {
        public ErrorFindingResultsException(string message) : base(message) { }
        public ErrorFindingResultsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
