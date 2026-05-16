using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosLoginController : ControllerBase
{
    //requisitos funcionais: Login de usuários com validação de email e senha, Geração de tokens JWT para autenticação e
    //Controle de permissões baseado no tipo de usuário
}
