using System.Security.Claims;

namespace BlogPessoal.Middlewares.Extensions;
public static class ClaimsPrincipalExtensions
{
    //verifca se é o usário logado para suas permissões
    public static int ObterIdUsuarioLogado(this ClaimsPrincipal user)
    {
        var claimValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(claimValue) || !int.TryParse(claimValue, out int userLogadoId))
            throw new UnauthorizedAccessException("Id do usuário inválido ou não existe.");

        return userLogadoId;
    }
}
