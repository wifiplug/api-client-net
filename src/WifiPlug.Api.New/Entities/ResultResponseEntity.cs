using Newtonsoft.Json;

namespace WifiPlug.Api.New.Entities
{
    public class ResultResponseEntity<TEntity>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the cursor for this page of results, if any.
        /// </summary>
        [JsonProperty("cursor")]
        public string Cursor { get; set; }

        /// <summary>
        /// Gets or sets the limit for each page.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the result objects.
        /// </summary>
        [JsonProperty("results")]
        public TEntity[] Results { get; set; }

        /// <summary>
        /// Gets or sets the total number of results, not just those on this page.
        /// </summary>
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }
        #endregion
    }
}
