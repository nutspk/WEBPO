using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WEBPO.Domain.UnitOfWork.Collections
{
    public interface IPagedList<T>
    {
        int IndexFrom { get; }
        int PageIndex { get; }
        int PageSize { get; }
        [JsonProperty("recordsTotal")]
        int TotalCount { get; }
        [JsonProperty("recordsFiltered")]
        int TotalPages { get; }
        [JsonProperty("data")]
        IList<T> Items { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
