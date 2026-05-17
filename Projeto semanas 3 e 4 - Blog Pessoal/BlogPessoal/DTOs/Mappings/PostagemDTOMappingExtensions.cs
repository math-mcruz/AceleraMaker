using BlogPessoal.DTOs.Postagens;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

public static class PostagemDTOMappingExtensions
{
    //entrada dos dados para mandar pro banco
    public static Postagem? RequestToPost(this PostagemRequestDTO postRequestDto)
    {
        if (postRequestDto is null)
            return null;

        return new Postagem
        {
            Titulo = postRequestDto.Titulo,
            Texto = postRequestDto.Texto,
            Data = postRequestDto.Data,
            UsuarioId = postRequestDto.UsuarioId,
            TemaId = postRequestDto.TemaId
        };
    }

    public static Postagem? ResponseToPost(this PostagemResponseDTO postResponseDto)
    {
        if (postResponseDto is null)
            return null;

        return new Postagem
        {
            PostagemId = postResponseDto.PostagemId,
            Titulo = postResponseDto.Titulo,
            Texto = postResponseDto.Texto,
            Data = postResponseDto.Data,
            UsuarioId = postResponseDto.UsuarioId,
            TemaId = postResponseDto.TemaId
        };
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
            NomeAutor = post.Usuario.Nome,
            NomeTema = post.Tema.Nome
        };
    }

    //saida de uma lista de postagens
    public static IEnumerable<PostagemResponseDTO> ToPostagemDTOList(this IEnumerable<Postagem> post)
    {
        if (post is null || !post.Any())
            return new List<PostagemResponseDTO>();

        return post.Select(postagem => new PostagemResponseDTO
        {
            PostagemId = postagem.PostagemId,
            Titulo = postagem.Titulo,
            Texto = postagem.Texto,
            Data = postagem.Data,
            NomeAutor = postagem.Usuario.Nome,
            NomeTema = postagem.Tema.Nome
        });
    }
}
