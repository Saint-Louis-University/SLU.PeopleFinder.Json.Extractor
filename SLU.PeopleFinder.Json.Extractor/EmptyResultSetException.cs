namespace SLU.PeopleFinder.Json.Exceptions
{
    /// <summary>
    /// Custom exception to be thrown when PeopleFinder query was successful, but there were no results.
    /// </summary>
    class EmptyResultSetException: Exception
    {
        /// <summary>
        /// Constructor to print message and report query parameters
        /// </summary>
        /// <param name="field">Desired PeopleFinder field</param>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        public EmptyResultSetException(Extractor.Field field, string query, int resultPosition, int fieldIndex)
            : base("The result set was empty.", field, query, resultPosition, fieldIndex) { }
    }
}
