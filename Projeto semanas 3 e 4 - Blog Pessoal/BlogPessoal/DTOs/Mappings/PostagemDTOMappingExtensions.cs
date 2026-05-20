using BlogPessoal.DTOs.Postagens;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

public static class PostagemDTOMappingExtensions
{
    //entrada dos dados para mandar pro banco
    public static Postagem? RequestToPost(this PostagemRequestDTO postRequestDto, int userLogado)
    {
        if (postRequestDto is null)
            return null;

        return new Postagem
        {
            Titulo = postRequestDto.Titulo,
            Texto = postRequestDto.Texto,
            Data = postRequestDto.Data,
            UsuarioId = userLogado,
            TemaId = postRequestDto.TemaId
        };
    }

    public static void UpdateToPost(this Postagem postExistente, PostagemUpdateDTO postUpdateDto)
    {
        if (postUpdateDto is null) 
            return;

        postExistente.PostagemId = postUpdateDto.PostagemId;
        postExistente.Titulo = postUpdateDto.Titulo;
        postExistente.Texto = postUpdateDto.Texto;
        postExistente.Data = postUpdateDto.Data; 
        postExistente.TemaId = postUpdateDto.TemaId;
    }

    //saida dos dados para enviar para o usuário novamente
    public static PostagemResponseDTO? ToPostResponseDTO(this Postagem post)
    {
        if (post is null)
            return null;

        return new PostagemResponseDTO
        {
            PostagemId = post.PostagemId,
            Titulo = post.Titulo,
            Texto = post.Texto,
            Data = post.Data,
            NomeAutor = post.Usuario?.UserName ?? "Autor anônimo",
            NomeTema = post.Tema?.Nome ?? "Tema Desconhecido"
        };
    }

    //saida de uma lista de postagens
    public static IEnumerable<PostagemResponseDTO> ToPostagemDTOList(this IEnumerable<Postagem> post)
    {
        if (post is null || !post.Any())
            return new List<PostagemResponseDTO>();

        return post.Select(postagem => postagem.ToPostResponseDTO()!);
    }
}
