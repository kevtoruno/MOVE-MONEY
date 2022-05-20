using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Core;

public class PagedList<T>: List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public PagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        this.PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count/ (double) pageSize);
        this.AddRange(items);
    }

    public static async Task<PagedList<T>> CreatePaginatedListAsync (IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();

        int skipUntil = (pageNumber - 1) * pageSize;

        var items = await source.Skip(skipUntil).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count ,pageNumber, pageSize);
    }
}