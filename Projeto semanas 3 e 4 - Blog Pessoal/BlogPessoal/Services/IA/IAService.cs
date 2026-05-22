using BlogPessoal.DTOs.IA;
using System.Text.Json;

namespace BlogPessoal.Services.IA;

public class IAService : IIAService
{
    private readonly GeminiService _geminiService;

    public IAService(GeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    public async Task<ResultadoIADTO> GerarResumoAsync(string texto)
    {

        if (string.IsNullOrWhiteSpace(texto))
        {
            return new ResultadoIADTO
            {
                Resumo = "Sem resumo",
                Tags = "Nenhuma",
                Categoria = "Geral"
            };
        }
        try
        {
            var prompt = PromptBuilder.PromptResumo(texto);
            var jsonResposta = await _geminiService.EnviarRequisicaoAsync(prompt);

            var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var resultado = JsonSerializer.Deserialize<ResultadoIADTO>(jsonResposta, opcoes);

            return resultado ?? new ResultadoIADTO();
        }
        catch (Exception)
        {
            return new ResultadoIADTO
            {
                Resumo = "Resumo indisponível no momento.",
                Tags = "Falha na IA",
                Categoria = "Geral"
            };
        }
    }
}
