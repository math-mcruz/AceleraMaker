using dotnet.DTOs.Clientes;
using dotnet.Service.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _cliService;

        public ClienteController(IClienteService cliService)
        {
            _cliService = cliService;
        }

        [HttpGet("{id:int}")]
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
           
        
        [HttpPost]
        public IActionResult Post(ClienteRequestDTO cliente)
        {
           try
            {
                var cliCadastro =  _cliService.Cadastrar(cliente);
                return StatusCode(StatusCodes.Status201Created, cliCadastro);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro inesperado no servidor.", Detalhes = e.Message });
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult Post(int id, ClienteUpdateDTO cliente)
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
        [HttpDelete("{id:int}")]
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
            catch (Exception e)
            {
                return StatusCode(500, new { erro = "Ocorreu um erro inesperado no servidor.", Detalhes = e.Message });
            }
        }
    }    
}