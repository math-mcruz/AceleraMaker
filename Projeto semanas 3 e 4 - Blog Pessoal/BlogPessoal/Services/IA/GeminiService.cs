using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlogPessoal.Services.IA;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _endpoint;

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _endpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-3.5-flash:generateContent";
        _apiKey = configuration["Gemini:ApiKey"];
        //_endpoint = configuration["Gemini:Endpoint"];
    }

    public async Task<string> EnviarRequisicaoAsync(string prompt)
    {
        var requestUrl = $"{_endpoint}?key={_apiKey}";
        //request que o Gemini pede
        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            var erroIA = await response.Content.ReadAsStringAsync();
            throw new Exception($"Detalhe do Erro 400: {erroIA}");
        }

        var responseString = await response.Content.ReadAsStringAsync();

        using var jsonDoc = JsonDocument.Parse(responseString);
        var textoExtraido = jsonDoc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();

        return textoExtraido ?? "{}";
    }
}
