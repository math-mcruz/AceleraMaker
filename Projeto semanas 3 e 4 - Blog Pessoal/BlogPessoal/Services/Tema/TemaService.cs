using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Mappings;
using BlogPessoal.DTOs.Temas;
using BlogPessoal.Repositories.UnitsOfWork;
using BlogPessoal.Services.Tema;

namespace BlogPessoal.Services.Temas;

public class TemaService : ITemaService
{
    private readonly IUnitOfWork _uof;

    public TemaService(IUnitOfWork uof)
    {
        _uof = uof;
    }

    public async Task<IEnumerable<TemaResponseDTO>> GetAllAsync()
    {
        var temas = await _uof.TemaRepository.GetAllAsync();

        if (temas is null || !temas.Any())
            throw new KeyNotFoundException("Não existem temas criados");

        return temas.ToTemaDTOList();
    }

    public async Task<TemaResponseDTO> CreateAsync(TemaRequestDTO temaRequestDto)
    {
        if (temaRequestDto is null)
            throw new ArgumentException("Dados inválidos");

        var tema = temaRequestDto.RequestToTema();

        var temaCriado = _uof.TemaRepository.Create(tema);
        await _uof.CommitAsync();

        return temaCriado.ToTemaResponseDTO();
    }

    public async Task<TemaResponseDTO> UpdateAsync(int id, TemaUpdateDTO temaUpdateDto)
    {
        if (id != temaUpdateDto.TemaId)
            throw new ArgumentException("O ID da URL não coincide com o ID do corpo."); 

        var temaExiste = await _uof.TemaRepository.GetAsync(t => t.TemaId == id);
        if (temaExiste is null)
            throw new KeyNotFoundException("Tema não encontrado."); 

        var tema = temaUpdateDto.UpdateToTema();
        var temaAtualizado = _uof.TemaRepository.Update(tema);
        await _uof.CommitAsync();

        return temaAtualizado.ToTemaResponseDTO();
    }

    public async Task DeleteAsync(int id)
    {
        var tema = await _uof.TemaRepository.GetAsync(c => c.TemaId == id);

        if (tema is null)
            throw new KeyNotFoundException("Tema não encontrado.");

        var postagemVinculada = await _uof.PostagemRepository.GetAsync(p => p.TemaId == id);

        if (postagemVinculada is not null)
            throw new InvalidOperationException("Existem postagens vinculadas ao Tema.");

        _uof.TemaRepository.Delete(tema);
        await _uof.CommitAsync();
    }
}