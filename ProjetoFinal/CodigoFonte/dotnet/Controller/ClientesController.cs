using dotnet.Service.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace dotnet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _cliService;

        public ClientesController(IClienteService cliService)
        {
            _cliService = cliService;
        }

        [HttpGet("{id:int}")]
        public IActionResult GET(int id)
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
           
        /*
        [HttpPost]
        public IActionResult Post(ClienteRequestDTO cliente)
        {
           
        }
        [HttpPut("{id:int}")]
        public IActionResult Post(int id, ClienteUpdateDTO cliente)
        {
           
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
           
        }*/
    }    
}