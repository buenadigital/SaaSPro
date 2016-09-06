using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SaaSPro.Common
{
    public interface IPagedList : IEnumerable
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }

        bool HasResults { get; }
        int Page { get; }
    }

    public interface IPagedList<T> : IPagedList, IList<T>
    {
    }

    /// <summary>
    /// A tried and tested PagedList implementation
    /// </summary>
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            this.TotalCount = totalCount;
            this.TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            this.AddRange(source.ToList());
        }
        
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage => (PageIndex > 0);
        public bool HasNextPage => (PageIndex + 1 < TotalPages);

        public bool HasResults => TotalCount > 0;
        public int Page => PageIndex + 1; // For display purposes
    }
}