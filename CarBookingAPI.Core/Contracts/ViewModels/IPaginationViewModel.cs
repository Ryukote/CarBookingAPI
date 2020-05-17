namespace CarBookingAPI.Core.Contracts.ViewModels
{
    /// <summary>
    /// Object for querying data.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public interface IPaginationViewModel<TViewModel>
        where TViewModel : IViewModel
    {
        /// <summary>
        /// Number of records to skip after querying.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Number of records to take after querying.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Querying data.
        /// </summary>
        public TViewModel QueryData { get; set; }
    }
}
