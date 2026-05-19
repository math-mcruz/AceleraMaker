using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogPessoal.Middlewares.Filters;

/*


Eu sei que não é aqui mas vou deixar a exigencia de filtro -------------------*********************************




Criar um filtro para interceptar e validar o token JWT em todas as requisições protegidas.
*/

public class ApiLoggingFilter: IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //esse método executa antes do método action
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }
}
