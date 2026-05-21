using BlogPessoal.DTOs;
using BlogPessoal.DTOs.Temas;

namespace BlogPessoal.Services.Tema;
public interface ITemaService
{
    Task<IEnumerable<TemaResponseDTO>> GetAllAsync();
    Task<TemaResponseDTO> CreateAsync(TemaRequestDTO temaRequestDto);
    Task<TemaResponseDTO> UpdateAsync(int id, TemaUpdateDTO temaUpdateDto);
    Task DeleteAsync(int id);
}
