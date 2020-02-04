using System;

namespace SearchFight.Shared.Exceptions
{
    public class ErrorHandlinghHttpException : GenericException
    {
        public ErrorHandlinghHttpException(string message) : base(message) { }
        public ErrorHandlinghHttpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
