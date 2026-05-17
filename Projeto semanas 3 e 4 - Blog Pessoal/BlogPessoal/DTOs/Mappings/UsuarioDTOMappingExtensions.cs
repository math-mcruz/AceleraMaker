using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;

namespace BlogPessoal.DTOs.Mappings;

public static class UsuarioDTOMappingExtensions
{
    //entrada dos dados para mandar pro banco
    public static Usuario? RequestToUsuario(this UsuarioRequestDTO usuRequestDto)
    {
        if (usuRequestDto is null)
            return null;

        return new Usuario
        {
            Nome = usuRequestDto.Nome,
            Email = usuRequestDto.Email,
            Senha = usuRequestDto.Senha
        };
    }

    public static Usuario? ResponseToUsuario(this UsuarioResponseDTO usuResponseDto)
    {
        if (usuResponseDto is null)
            return null;

        return new Usuario
        {
            UsuarioId = usuResponseDto.UsuarioId,
            Nome = usuResponseDto.Nome,
            Email = usuResponseDto.Email
        };
    }

    //saida dos dados para enviar para o usuário novamente
    public static UsuarioResponseDTO? ToUsuarioDTO(this Usuario usuario)
    {
        if (usuario is null)
            return null;

        return new UsuarioResponseDTO
        {
            UsuarioId = usuario.UsuarioId,
            Nome = usuario.Nome,
            Email = usuario.Email,
        };
    }

}
