using Application.Queries.Generic;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;

namespace Application.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(IConfiguration Configuration)
        {
            _baseUri = Configuration.GetSection("URL_GEN")["URI_BASE"];
        }
        public string GetAllObjectsNextPageUri(PaginationQuery pagination = null)
        {
            if (pagination == null)
            {
                return new Uri(_baseUri).ToString();
            }
            string nextPageUri = QueryHelpers.AddQueryString(_baseUri, nameof(pagination.PageNumber), (pagination.PageNumber + 1).ToString());
            nextPageUri = QueryHelpers.AddQueryString(nextPageUri, nameof(pagination.PageSize), pagination.PageSize.ToString());

            return nextPageUri;
        }

        public string GetAllObjectsPrevPageUri(PaginationQuery pagination = null)
        {
            if (pagination == null)
            {
                return new Uri(_baseUri).ToString();
            }

            string prevPageUri = QueryHelpers.AddQueryString(_baseUri, nameof(pagination.PageNumber), (pagination.PageNumber - 1).ToString());
            prevPageUri = QueryHelpers.AddQueryString(prevPageUri, nameof(pagination.PageSize), pagination.PageSize.ToString());

            return prevPageUri;
        }

        public Uri GetObjectById(int id)
        {
            return new Uri(_baseUri + $"{id}");
        }
    }
}
