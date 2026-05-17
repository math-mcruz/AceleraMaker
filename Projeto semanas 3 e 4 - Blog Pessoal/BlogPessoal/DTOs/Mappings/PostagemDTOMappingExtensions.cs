using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

public static class PostagemDTOMappingExtensions
{
    public static PostagemDTO? ToPostagemDTO(this Postagem postagem)
    {
        if (postagem is null)
            return null;

        return new PostagemDTO
        {
            PostagemId = postagem.PostagemId,
            Titulo = postagem.Titulo,
            Texto = postagem.Texto,
            Data = postagem.Data,
            UsuarioId = postagem.UsuarioId,
            TemaId = postagem.TemaId
        };
    }

    public static Postagem? ToPostagem(this PostagemDTO postagemDto)
    {
        if (postagemDto is null)
            return null;

        return new Postagem
        {
            PostagemId = postagemDto.PostagemId,
            Titulo = postagemDto.Titulo,
            Texto = postagemDto.Texto,
            Data = postagemDto.Data,
            UsuarioId = postagemDto.UsuarioId,
            TemaId = postagemDto.TemaId
        };
    }

    public static IEnumerable<PostagemDTO> ToPostagemDTOList(this IEnumerable<Postagem> postagens)
    {
        if (postagens is null || !postagens.Any())
            return new List<PostagemDTO>();

        return postagens.Select(postagem => new PostagemDTO
        {
            PostagemId = postagem.PostagemId,
            Titulo = postagem.Titulo,
            Texto = postagem.Texto,
            Data = postagem.Data,
            UsuarioId = postagem.UsuarioId,
            TemaId = postagem.TemaId
        });
    }
}
