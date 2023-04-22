namespace Ping.Core.Extensions.Pagination;

public class PaginationResult<TModel>
    where TModel : class
{
    public PaginationResult(int total, List<TModel> data, int page, int perPage, List<int> pageNumberList, bool previousButtonDisabled, bool nextButtonDisabled)
    {
        this.Total = total;
        this.Data = data;
        this.Page = page;
        this.PerPage = perPage;
        this.PageNumbers = pageNumberList;
        this.PreviousButtonDisabled = previousButtonDisabled;
        this.NextButtonDisabled = nextButtonDisabled;
    }

    public int Total { get; set; }
    public List<TModel> Data { get; set; } = new List<TModel>();
    public int PerPage { get; set; }
    public int Page { get; set; }
    public List<int> PageNumbers { get; set; }
    public bool PreviousButtonDisabled { get; set; }
    public bool NextButtonDisabled { get; set; }
}