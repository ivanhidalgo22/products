using System.Collections.Generic;

namespace Sample.Marketplace.Products.API.ViewModel
{
    /// <summary>
    /// Builds a response message with page properties.
    /// </summary>
    /// <typeparam name="TEntity">Response message.</typeparam>
    public class PaginatedElementsViewModel<TEntity> where TEntity : class
    {
        /// <summary>
        /// Page number property.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Page size property.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Number of registers.
        /// </summary>
        public long Count { get; private set; }

        /// <summary>
        /// Response Message.
        /// </summary>
        public IEnumerable<TEntity> Data { get; private set; }

        /// <summary>
        /// Builds a response message with page properties.
        /// </summary>
        /// <param name="pageIndex">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="count">Number of registers.</param>
        /// <param name="data">Response Message.</param>
        public PaginatedElementsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}