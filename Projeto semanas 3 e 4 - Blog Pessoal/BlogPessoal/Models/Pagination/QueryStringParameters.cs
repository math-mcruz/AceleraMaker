namespace BlogPessoal.Models.Pagination;

//paginação gênerica
public class QueryStringParameters
{
    const int maxMaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = maxMaxPageSize;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value < maxMaxPageSize)? value : maxMaxPageSize;//limitar em 50 para não sobrecarregar
        }
    }
}