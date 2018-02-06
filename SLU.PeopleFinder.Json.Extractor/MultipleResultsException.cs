namespace SLU.PeopleFinder.Json.Exceptions
{
    /// <summary>
    /// Custom exception to be thrown when there were multiple results, but the requested result position was default.
    /// </summary>
    class MultipleResultsException: Exception
    {
        /// <summary>
        /// Constructor to print message and report query parameters
        /// </summary>
        /// <param name="field">Desired PeopleFinder field</param>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        public MultipleResultsException(Extractor.Field field, string query, int resultPosition, int fieldIndex)
            : base("The requested result position was default, and there was more than one result.", field, query, resultPosition, fieldIndex) { }
    }
}
