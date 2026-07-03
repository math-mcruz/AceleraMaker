using dotnet.DTOs.Clientes;
using dotnet.Infrastructure;
using dotnet.Service.Clientes;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text;

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
            var cliResponseDto =  _cliService.Consultar(id);
            return Ok(cliResponseDto);
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