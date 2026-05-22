using BlogPessoal.DTOs.Temas;
using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

//não vou usar o AutoMapper
public static class TemaDTOMappingExtensions
{
    //entrada dos dados para mandar pro banco
    public static Tema? RequestToTema(this TemaRequestDTO temaDto)
    {
        if (temaDto is null)
            return null;

        return new Tema
        {
            Nome = temaDto.Nome
        };
    }
    //criei esse método para fazer o put
    public static Tema? UpdateToTema(this TemaUpdateDTO temaUpdateDto)
    {
        if (temaUpdateDto is null)
            return null;

        return new Tema
        {
            TemaId = temaUpdateDto.TemaId,
            Nome = temaUpdateDto.Nome
        };
    }

    //saida dos dados para enviar para o usuário novamente
    public static TemaResponseDTO? ToTemaResponseDTO(this Tema tema)
    {
        if(tema is null)
            return null;

        return new TemaResponseDTO
        {
            TemaId = tema.TemaId,
            Nome = tema.Nome,
        };
    }


    public static IEnumerable<TemaResponseDTO> ToTemaDTOList(this IEnumerable<Tema> temas)
    {
        if(temas is null || !temas.Any())
            return new List<TemaResponseDTO>();

        return temas.Select(tema => new TemaResponseDTO
        {
            TemaId = tema.TemaId,
            Nome = tema.Nome,
        });
    }
}
