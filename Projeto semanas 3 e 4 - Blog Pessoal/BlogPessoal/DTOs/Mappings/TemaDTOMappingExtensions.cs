using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

//não vou usar o AutoMapper por questões de melhor desempenho
public static class TemaDTOMappingExtensions
{
    public static TemaDTO? ToTemaDTO(this Tema tema)
    {
        if(tema is null)
            return null;

        return new TemaDTO
        {
            TemaId = tema.TemaId,
            Nome = tema.Nome
        };
    }

    public static Tema? ToTema(this TemaDTO temaDto)
    {
        if (temaDto is null)
            return null;

        return new Tema
        {
            TemaId = temaDto.TemaId,
            Nome = temaDto.Nome
        };
    }

    public static IEnumerable<TemaDTO> ToTemaDTOList(this IEnumerable<Tema> temas)
    {
        if(temas is null || !temas.Any())
            return new List<TemaDTO>();

        return temas.Select(tema => new TemaDTO
        {
            TemaId = tema.TemaId,
            Nome = tema.Nome
        });

    }
}
