namespace BlogPessoal.Models.Pagination;

public class PostagensParameters : QueryStringParameters
{
    // ? pois a consulta é ou/e então pode ser um ou os dois, assim eles podem ser nulos
    public int? Autor {  get; set; }
    public int? Tema {  get; set; }
}
