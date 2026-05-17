namespace BlogPessoal.Models.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    //quando tem uma página anterior
    public bool HasPrevious => CurrentPage > 1;
    //para ver se tem próxima página
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList(List<T> itens, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        //calculo do total de páginas é o tatal de itens dividido pelo tamanho da página
        TotalCount = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(itens);
    }

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        //busca os elementos da página atual
        var itens = source.Skip(pageNumber - 1).Take(pageSize).ToList();
        //retorna a paginação
        return new PagedList<T>(itens, count, pageNumber, pageSize);
    }
}
