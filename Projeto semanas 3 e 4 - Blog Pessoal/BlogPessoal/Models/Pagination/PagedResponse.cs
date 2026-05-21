namespace BlogPessoal.Models.Pagination;

public class PagedResponse<T>
{
    // A lista de dados fica isolada aqui dentro
    public IEnumerable<T> Dados { get; set; }

    // Os metadados da paginação
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedResponse(IEnumerable<T> dados, int count, int pageNumber, int pageSize)
    {
        Dados = dados;
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}
