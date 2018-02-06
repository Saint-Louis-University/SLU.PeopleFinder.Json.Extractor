namespace SLU.PeopleFinder.Json.Exceptions
{
    /// <summary>
    /// Base exception class from which custom exceptions inherit.
    /// Provides a constructor to standardize exception messages with the PeopleFinder-query parameters when the exception occured.
    /// </summary>
    class Exception: System.Exception
    {
        private static string ParameterizedMessage(string message, Extractor.Field field, string query, int resultPosition, int fieldIndex)
        {
            return string.Format("{0} field={1}; query={2}; resultPosition={3}; fieldIndex={4}.", 
                message, field.ToString(), query, resultPosition, fieldIndex);
        }

        public Exception(string message, Extractor.Field field, string query, int resultPosition, int fieldIndex)
            : base(ParameterizedMessage(message, field, query, resultPosition, fieldIndex)) { }
    }
}
