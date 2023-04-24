using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Ping.Core.Extensions.Pagination;

public static class Pagination
{
    public const string SORT_ASC = "ASC";
    public const string SORT_DESC = "DESC";

    public static PaginationResult<TModel> Paginate<TModel>(this IQueryable<TModel> entities, PaginationQuery paginationQuery, Expression<Func<TModel, bool>> searchFilter = null)
        where TModel : class
    {
        var (page, perPage, search, sortColumn, sortDirection) = paginationQuery;
        if (search is not null)
            entities = entities.Where(searchFilter);

        var total = entities.Count();

        if (!string.IsNullOrEmpty(sortColumn))
        {
            var sortColumnList = sortColumn.Split(",").Select(s => $"{s} {sortDirection}");
            var orderByStr = string.Join(",", sortColumnList);
            entities = entities.OrderBy(orderByStr);
        }

        var data = entities
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .ToList();

        var totalPages = (int)Math.Ceiling(total / (double)perPage);
        var pageNumberList = GetPageNumberList(page, totalPages);
        var previousButtonDisabled = page - 1 <= 0;
        var nextButtonDisabled = page + 1 > totalPages;

        return new PaginationResult<TModel>(total, data, page, perPage, pageNumberList, previousButtonDisabled, nextButtonDisabled);
    }

    private static List<int> GetPageNumberList(int page, int totalPages)
    {
        var start = 1;
        var end = totalPages;

        if (page - 2 <= 0)
            start = 1;
        else
            start = page - 2;

        if (start + 4 > totalPages)
            end = totalPages;
        else
            end = start + 4;

        return Enumerable.Range(start, end - start + 1).ToList();
    }
}
