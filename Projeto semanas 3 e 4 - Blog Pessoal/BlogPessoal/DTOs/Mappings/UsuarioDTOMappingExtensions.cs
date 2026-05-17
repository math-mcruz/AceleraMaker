using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

public static class UsuarioDTOMappingExtensions
{
    public static UsuarioDTO? ToUsuarioDTO(this Usuario usuario)
    {
        if (usuario is null)
            return null;

        return new UsuarioDTO
        {
            UsuarioId = usuario.UsuarioId,
            Nome = usuario.Nome,
            //sei que não pode email e senha, mas vou colocar por enquanto até fazer a parte da segurança
            Email = usuario.Email,
            Senha = usuario.Senha
        };
    }

    public static Usuario? ToUsuario(this UsuarioDTO usuarioDto)
    {
        if (usuarioDto is null)
            return null;

        return new Usuario
        {
            UsuarioId = usuarioDto.UsuarioId,
            Nome = usuarioDto.Nome,
            //sei que não pode email e senha, mas vou colocar por enquanto até fazer a parte da segurança
            Email = usuarioDto.Email,
            Senha = usuarioDto.Senha
        };
    }
}
