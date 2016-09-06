using AutoMapper;
using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using System.Collections.Generic;

namespace SaaSPro.Web.Common
{
    public static class AutoMapperExtensions
    {
        public static IPagedList<TDestination> MapPaged<TSource, TDestination>(
            this IMappingEngine mapper, IPagedList<TSource> source)
        {
            Ensure.Argument.NotNull(source, nameof(source));
            return new PagedList<TDestination>(mapper.Map<IEnumerable<TDestination>>(source), source.PageIndex, source.PageSize, source.TotalCount);
        }
    }
}