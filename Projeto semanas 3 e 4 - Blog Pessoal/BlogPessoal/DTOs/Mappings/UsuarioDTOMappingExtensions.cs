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
            UserName = usuRequestDto.Username,
            Email = usuRequestDto.Email,
            PasswordHash = usuRequestDto.Senha
        };
    }

    public static Usuario? ResponseToUsuario(this UsuarioResponseDTO usuResponseDto)
    {
        if (usuResponseDto is null)
            return null;

        return new Usuario
        {
            Id = usuResponseDto.UsuarioId,
            UserName = usuResponseDto.Username,
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
            UsuarioId = usuario.Id,
            Username = usuario.UserName,
            Email = usuario.Email,
        };
    }

}
