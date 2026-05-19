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

    public static Usuario? UpdateToUsuario(this UsuarioUpdateDTO usuUpdateDto)
    {
        if (usuUpdateDto is null)
            return null;

        return new Usuario
        {
            Id = usuUpdateDto.Id,
            UserName = usuUpdateDto.Username,
            Email = usuUpdateDto.Email,
            PasswordHash = usuUpdateDto.Senha
        };
    }

    //saida dos dados para enviar para o usuário novamente
    public static UsuarioResponseDTO? ToUsuarioDTO(this Usuario usuario)
    {
        if (usuario is null)
            return null;

        return new UsuarioResponseDTO
        {
            Id = usuario.Id,
            Username = usuario.UserName,
            Email = usuario.Email,
        };
    }
}
