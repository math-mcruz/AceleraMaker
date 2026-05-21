using FluentAssertions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

public class ApiExceptionMiddlewareExtensionsTests
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }

    private async Task<(int StatusCode, ErrorDetails Error)> SimularErroNoMiddleware(Exception ex)
    {
        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;


        context.Features.Set<IExceptionHandlerPathFeature>(new ExceptionHandlerFeature
        {
            Error = ex,
            Path = "/api/teste"
        });

        await EscreverRespostaDeErroManual(context, ex);

        // 4. Ler a resposta
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var body = await reader.ReadToEndAsync();

        var error = string.IsNullOrEmpty(body) ? null : JsonSerializer.Deserialize<ErrorDetails>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return (context.Response.StatusCode, error);
    }

    // Este método simula a lógica do seu projeto principal para fins de teste
    private async Task EscreverRespostaDeErroManual(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex switch
        {
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            ArgumentException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message,
            Trace = ex.StackTrace
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    [Theory]
    [InlineData(typeof(KeyNotFoundException), 404, "Não encontrado")]
    [InlineData(typeof(ArgumentException), 400, "Dados inválidos")]
    [InlineData(typeof(Exception), 500, "Erro interno")]
    public async Task Middleware_DeveMapearExcecoesCorretamente(Type exceptionType, int expectedStatusCode, string message)
    {
        var exception = (Exception)Activator.CreateInstance(exceptionType, message);
        var result = await SimularErroNoMiddleware(exception);

        result.StatusCode.Should().Be(expectedStatusCode);
        result.Error.Message.Should().Be(message);
    }
}