using BlogPessoal.Models;

namespace BlogPessoal.Repositories.Usuarios;

public interface IUsuarioRepository
{
    Usuario CadastrarUsuario();
    Usuario AtualizarUsuario();
    Usuario DeletarUsuario();
    
    //como fazer?
    //POST /api/usuarios/login
    //Autenticar o usuário e gerar um token JWT.
}
