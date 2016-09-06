using System;
using SaaSPro.Domain;

namespace SaaSPro.Services.ViewModels
{
    public class PagingCommand
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public UserType UserType { get; set; }
        public Guid CustomerId { get; set; }
        /// <summary>
        /// Creates a new <see cref="PagingCommand"/> instance.
        /// </summary>
        public PagingCommand()
        {
            // set up sensible defaults. Page index is 1 based for consumer
            Page = 1;
            PageSize = 10;
        }

        /// <summary>
        /// Returns the zero based page index
        /// </summary>
        public int PageIndex
        {
            get
            {
                return Page > 0 ? Page - 1 : 0;
            }
        }
    }
}