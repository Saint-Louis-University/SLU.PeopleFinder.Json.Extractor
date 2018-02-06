namespace SLU.PeopleFinder.Json.Exceptions
{
    /// <summary>
    /// Custom exception to be thrown when the field was not defined for the requested result.
    /// </summary>
    class FieldUndefinedException: Exception
    {
        /// <summary>
        /// Constructor to print message and report query parameters
        /// </summary>
        /// <param name="field">Desired PeopleFinder field</param>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        public FieldUndefinedException(Extractor.Field field, string query, int resultPosition, int fieldIndex)
            : base("Field not defined in PeopleFinder for requested result.", field, query, resultPosition, fieldIndex) { }
    }
}
