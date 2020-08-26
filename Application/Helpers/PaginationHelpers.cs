using Application.Queries.Generic;
using Application.Response.Generic;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationQuery paginationQuery, List<T> result)
        {
            string nextPage = paginationQuery.PageNumber >= 1 ? uriService
                .GetAllObjectsNextPageUri(paginationQuery)
                : null;

            string prevPage = paginationQuery.PageNumber - 1 >= 1 ? uriService
                .GetAllObjectsPrevPageUri(paginationQuery)
                : null;

            return new PagedResponse<T>()
            {
                Data = result,
                PageNumber = paginationQuery.PageNumber >= 1 ? paginationQuery.PageNumber : (int?)null,
                PageSize = paginationQuery.PageSize >= 1 ? paginationQuery.PageSize : (int?)null,
                NextPage = result.Any() ? nextPage : null,
                PreviousPage = prevPage
            };
        }

        public static async Task<ICollection<T>> ToPagedListAsync<T>(this IQueryable<T> objects, int pageNumber, int pageSize)
        {
            int skipValue = (pageNumber - 1) * pageSize;
            return await objects.Skip(skipValue).Take(pageSize).ToListAsync();

        }
    }
}
