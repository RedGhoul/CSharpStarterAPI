using Application.Queries.Generic;
using System;

namespace Application.Services
{
    public interface IUriService
    {
        Uri GetObjectById(int id);
        public string GetAllObjectsNextPageUri(PaginationQuery pagination = null);
        public string GetAllObjectsPrevPageUri(PaginationQuery pagination = null);
    }
}
