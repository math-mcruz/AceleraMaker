using BlogPessoal.DTOs.Status;
using BlogPessoal.DTOs.Usuarios;
using BlogPessoal.Models;

namespace BlogPessoal.Services.Usuario;

public interface IUsuarioService
{
    Task<Response> CadastrarAsync(UsuarioRequestDTO userCadastro);

    Task<(string Token, DateTime Expiration)> LoginAsync(UsuarioLogin userLogin);

    Task AtualizarPerfilAsync(int id, UsuarioUpdateDTO userUpdateDto, int usuarioLogadoId, bool ehAdmin);

    Task ExcluirContaAsync(int id, int usuarioLogadoId, bool ehAdmin);
}
