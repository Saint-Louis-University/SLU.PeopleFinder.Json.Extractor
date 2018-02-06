namespace SLU.PeopleFinder.Json.Exceptions
{
    /// <summary>
    /// Custom exception to be thrown when the requested result position was greater than the total number of results.
    /// </summary>
    class ResultPositionOutOfBoundsException: Exception
    {
        /// <summary>
        /// Constructor to print message and report query parameters
        /// </summary>
        /// <param name="field">Desired PeopleFinder field</param>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        public ResultPositionOutOfBoundsException(Extractor.Field field, string query, int resultPosition, int fieldIndex)
            : base("The requested result position was greater than the number of results available.", field, query, resultPosition, fieldIndex) { }
    }
}
