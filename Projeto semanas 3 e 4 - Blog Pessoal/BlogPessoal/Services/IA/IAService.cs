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

    public async Task<ResultadoIADTO> GerarResumoAsync(string conteudo)
    {
        if (string.IsNullOrWhiteSpace(conteudo))
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
            var prompt = PromptBuilder.PromptResumo(conteudo);
            var jsonResposta = await _geminiService.EnviarRequisicaoAsync(prompt);

            var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var resultado = JsonSerializer.Deserialize<ResultadoIADTO>(jsonResposta, opcoes);

            return resultado ?? new ResultadoIADTO();
        }
        catch (Exception e)
        {
            return new ResultadoIADTO
            {
                Resumo = "Resumo indisponível no momento.",
                Tags = e.Message, //Falha na IA
                Categoria = "Geral"
            };
        }
    }
}
