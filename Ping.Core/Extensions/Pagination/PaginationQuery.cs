namespace Ping.Core.Extensions.Pagination;

public record PaginationQuery(
    int page = 1,
    int perPage = 10,
    string search = null,
    string sortColumn = null,
    string sortDirection = Pagination.SORT_ASC);
