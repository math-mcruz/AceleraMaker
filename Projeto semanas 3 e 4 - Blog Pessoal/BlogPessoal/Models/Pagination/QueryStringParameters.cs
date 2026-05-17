namespace BlogPessoal.Models.Pagination;

//uma classe de paginação gênerica para poder usar em mais futuramente
public class QueryStringParameters
{
    const int maxMaxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize;
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
