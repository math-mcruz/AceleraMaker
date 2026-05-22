using BlogPessoal.DTOs.IA;

namespace BlogPessoal.Services.IA;

public interface IIAService
{
    Task<ResultadoIADTO> GerarResumoAsync(string texto);
}
