namespace SLU.PeopleFinder.Json
{
    /// <summary>
    /// This class can query PeopleFinder for JSON and extract a requested datum.
    /// </summary>
    public static class Extractor
    {
        /// <summary>
        /// All fields possible for a PeopleFinder result
        /// Note. dept, room, building, telephone, and office are not defined for all results.
        /// </summary>
        protected internal enum Field
        {
            surname,
            givenname,
            fullname,
            title,
            dept,
            affiliation,
            email,
            room,
            building,
            campus,
            id,
            telephone,
            office
        };

        /// <summary>
        /// Queries PeopleFinder and extracts the requested datum from the resulting JSON.
        /// </summary>
        /// <remarks>
        /// Structure of a resultSet:
        /// 
        /// resultSet
        ///     search_time
        ///     totalResultsAvailable
        ///     totalResultsReturned
        ///     firstResultPosition
        ///     result_serial
        ///     result[]
        ///         surname[]
        ///         givenname[]
        ///         fullname[]
        ///         title[]
        ///         dept[]*
        ///         affiliation[]
        ///         email[]
        ///         room[]*
        ///         building[]*
        ///         campus[]
        ///         id
        ///         telephone[]*
        ///         office[]*
        ///         
        /// An * denotes field is only available for some results.
        /// Arrays almost never have more than one entry.
        /// </remarks>
        /// <param name="field">Desired PeopleFinder field</param>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested datum</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        private static string Get(Field field, string query, int resultPosition = 0, int fieldIndex = 0)
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                var jsonSerial = wc.DownloadString("https://www.slu.edu/peoplefinder/json/json_index.php?q=" + query);
                var jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonSerial);

                int totalResultsAvailable = jsonObject.resultSet.totalResultsAvailable;

                // if results returned but there are none
                if (totalResultsAvailable == 0)
                    throw new Exceptions.EmptyResultSetException(field, query, resultPosition, fieldIndex);

                // if requested index is greater than the available results
                if (resultPosition > totalResultsAvailable)
                    throw new Exceptions.ResultPositionOutOfBoundsException(field, query, resultPosition, fieldIndex);

                // if requested index is default and there are multiple results
                if (resultPosition == 0 & totalResultsAvailable > 1)
                    throw new Exceptions.MultipleResultsException(field, query, resultPosition, fieldIndex);

                // correct indexing to be 0-based
                if (resultPosition != 0)
                    resultPosition--;
                
                try
                {
                    switch (field)
                    {
                        case Field.affiliation:
                            return jsonObject.resultSet.result[resultPosition].affiliation[fieldIndex];
                        case Field.building:
                            return jsonObject.resultSet.result[resultPosition].building[fieldIndex];
                        case Field.campus:
                            return jsonObject.resultSet.result[resultPosition].campus[fieldIndex];
                        case Field.dept:
                            return jsonObject.resultSet.result[resultPosition].dept[fieldIndex];
                        case Field.email:
                            return jsonObject.resultSet.result[resultPosition].email[fieldIndex];
                        case Field.fullname:
                            return jsonObject.resultSet.result[resultPosition].fullname[fieldIndex];
                        case Field.givenname:
                            return jsonObject.resultSet.result[resultPosition].givenname[fieldIndex];
                        case Field.id:
                            return jsonObject.resultSet.result[resultPosition].id;
                        case Field.office:
                            return jsonObject.resultSet.result[resultPosition].office[fieldIndex];
                        case Field.room:
                            return jsonObject.resultSet.result[resultPosition].room[fieldIndex];
                        case Field.surname:
                            return jsonObject.resultSet.result[resultPosition].surname[fieldIndex];
                        case Field.telephone:
                            return jsonObject.resultSet.result[resultPosition].telephone[fieldIndex];
                        case Field.title:
                            return jsonObject.resultSet.result[resultPosition].title[fieldIndex];
                    }
                }
                #pragma warning disable CS0168 // Variable is declared but never used
                catch(Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                #pragma warning restore CS0168 // Variable is declared but never used
                {
                    throw new Exceptions.FieldUndefinedException(field, query, resultPosition, fieldIndex);
                }
                
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the affiliation.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested affiliation</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetAffiliation(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.affiliation, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the building (not defined for all results).
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested building</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetBuilding(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.building, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the campus.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested campus</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetCampus(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.campus, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the department (not defined for all results).
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested department</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetDepartment(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.dept, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested email</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetEmail(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.email, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested full name</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetFullName(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.fullname, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the given name.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested given name</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetGivenName(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.givenname, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the SLUNet ID.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested SLUNet ID</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetId(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.id, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the office (not defined for all results).
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested office</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetOffice(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.office, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the room (not defined for all results).
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested room</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetRoom(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.room, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the surname.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested surname</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetSurname(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.surname, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the telephone (not defined for all results).
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested telephone</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetTelephone(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.telephone, query, resultPosition, fieldIndex);
        }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="resultPosition">Optional 1-based index of the desired result in the result set</param>
        /// <param name="fieldIndex">Optional 0-based index of the desired value within the requested field</param>
        /// <returns>Requested title</returns>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.EmptyResultSetException">Thrown when query was successful but there were no results.</exception>
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.FieldUndefinedException">Thrown when field was not defined in the JSON for the result.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.MultipleResultsException">Thrown when there were multiple results and result position was default.</exception>"
        /// <exception cref="SLU.PeopleFinder.Json.Exceptions.ResultPositionOutOfBoundsException">Thrown when the requested result position was greater than the total results.</exception>"
        public static string GetTitle(string query, int resultPosition = 0, int fieldIndex = 0)
        {
            return Get(Field.title, query, resultPosition, fieldIndex);
        }
    }
}
