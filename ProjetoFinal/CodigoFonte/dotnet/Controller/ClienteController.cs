using dotnet.DTOs.Clientes;
using dotnet.Service.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _cliService;

        public ClienteController(IClienteService cliService)
        {
            _cliService = cliService;
        }

        /// <summary>
        /// Consultar cliente.
        /// </summary>
        /// <remarks>
        ///**Observação:** 
        ///* **ID é obrigatório, tamanho máximo de 5 dígitos**  
        ///
        /// </remarks>
        /// <param name="id">ID do cliente a ser consultado.</param>
        /// <returns>Dados do cliente cadastro.</returns>
        /// <response code="200">Cliente encontrado com sucesso!</response>
        /// <response code="400">Falha: dado inválido.</response>
        /// <response code="404">Cliente não encontrado.</response>
        /// <response code="500">Problemas interno do sistema.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Get(int id)
        {
            try
            {
                var cliente =  _cliService.Consultar(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { erro = "Cliente não encontrado" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro inesperado no servidor."});
            }
        }
           
        /// <summary>
        /// Cadastrar novo cliente.
        /// </summary>
        /// <remarks>
        ///**Observações:** 
        ///* **Nome é obrigatório, tamanho máximo de 30 letras** 
        ///* **Telefone tem tamanho máximo de 18 caracteres** 
        ///* **E-mail tem tamanho máximo de 30 caracteres** 
        /// 
        /// Exemplo de requisição:
        ///
        ///     POST /api/clientes
        ///     {
        ///        "cli_Nome": "Aline Dutra",
        ///        "telefone": "(34) 97777-2222" 
        ///        "email": "aline@aceleramaker.com.br",
        ///     }
        ///
        /// </remarks>
        /// <param name="cliente">Dados do cadastro a ser criado.</param>
        /// <returns>Dados do cadastro criado.</returns>
        /// <response code="201">Cadastro criado com sucesso!</response>
        /// <response code="400">Falha, possíveis erros: dados inválidos ou formatação.</response>
        /// <response code="500">Problemas interno do sistema.</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Post(ClienteRequestDTO cliente)
        {
           try
            {
                var cliCadastro =  _cliService.Cadastrar(cliente);
                return StatusCode(StatusCodes.Status201Created, cliCadastro);
            }
            catch (Exception)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro inesperado no servidor."});
            }
        }

        /// <summary>
        /// Atualizar cliente.
        /// </summary>
        /// <remarks>
        ///**Observação:** 
        ///* **ID é obrigatório, tamanho máximo de 5 dígitos**  
        ///* **Telefone e E-mail são opcionais** 
        ///* **Telefone tem tamanho máximo de 18 caracteres** 
        ///* **E-mail tem tamanho máximo de 30 caracteres**  
        ///
        /// Exemplo de requisição:
        ///
        ///     PATCH /api/clientes/id
        ///     {
        ///        "cli_Id": 1,
        ///        "telefone": "(11) 98888-1111" 
        ///        "email": "luisguerreiro@aceleramaker.com.br",
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">ID do cadastro a ser atualizado.</param>
        /// <param name="cliente">Dados do cliente ser atualizado.</param>
        /// <returns>Dados do cliente atualizado.</returns>
        /// <response code="200">Cliente atualizado com sucesso!</response>
        /// <response code="400">Falha possíveis erros: dados inválidos ou formatação.</response>
        /// <response code="404">Cliente não encontrado.</response>
        /// <response code="500">Problemas interno do sistema.</response>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Patch(int id, ClienteUpdateDTO cliente)
        {
           try
            {
                var cliAtualizado =  _cliService.Atualizar(id, cliente);
                return Ok(cliAtualizado);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { erro = "Cliente não encontrado" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro inesperado no servidor."});
            }
        }

        /// <summary>
        /// Deletar cliente.
        /// </summary>
        /// <remarks>
        ///**Observação:** 
        ///* **ID é obrigatório, tamanho máximo de 5 dígitos**  
        ///
        /// </remarks>
        /// <param name="id">ID do cadastro a ser deletado.</param>
        /// <returns>Status de confirmação sem conteúdo.</returns>
        /// <response code="204">Cliente deletado com sucesso!</response>
        /// <response code="400">Falha: dado inválido.</response>
        /// <response code="404">Cliente não encontrado.</response>
        /// <response code="500">Problemas interno do sistema.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
           try
            {
                _cliService.Deletar(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { erro = "Cliente não encontrado" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro inesperado no servidor."});
            }
        }
    }    
}