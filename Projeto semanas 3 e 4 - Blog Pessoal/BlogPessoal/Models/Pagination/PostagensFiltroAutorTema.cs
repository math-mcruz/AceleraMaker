namespace BlogPessoal.Models.Pagination;

public class PostagensFiltroAutorTema : QueryStringParameters
{
    // ? pois a consulta é ou/e então pode ser um ou os dois, assim eles podem ser nulos
    public int? AutorId {  get; set; }
    public int? TemaId {  get; set; }
}
