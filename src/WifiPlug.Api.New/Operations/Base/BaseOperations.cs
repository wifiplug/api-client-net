namespace WifiPlug.Api.New.Operations.Base
{
    public abstract class BaseOperations
    {
        #region Properties
        /// <summary>
        /// Gets the API requestor to use for communicating with the API.
        /// </summary>
        protected IApiRequestor ApiRequestor { get; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new set of API operations.
        /// </summary>
        /// <param name="apiRequestor">The API requestor to use for communicating with the API.</param>
        protected BaseOperations(IApiRequestor apiRequestor)
        {
            ApiRequestor = apiRequestor;
        }
        #endregion
    }
}
