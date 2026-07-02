using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text;

namespace dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        //importa o .dll dinamico via P/Invoke
        [DllImport(@"D:\AceleraMaker\projetosAceleraMaker\ProjetoFinal\CodigoFonte\COBOL\SOURCE\CONSCLI.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void CONSCLI(byte[] argumento);
        //impota a função de inicialização do runtime COBOL
        [DllImport("libcob-4.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void cob_init(int argc, IntPtr argv);

        [HttpGet("{id}")]
        public IActionResult ConsultarCliente(int id)
        {
            //formata o ID para 5 posições e preenche os 37 bytes com espaços
            string idFormatado = id.ToString("D5");
            string payload = idFormatado.PadRight(37, ' ');

            //converte a string para um array de bytes
            byte[] memoriaCobol = Encoding.ASCII.GetBytes(payload);

            try
            {
                cob_init(0, IntPtr.Zero);
                //executa o arquivo .dll na memoria da API
                CONSCLI(memoriaCobol);

                //recebe o resultado do COBOL de volta para String
                string respostaCobol = Encoding.ASCII.GetString(memoriaCobol);
                string idRetorno = respostaCobol.Substring(0, 5);
                string nomeRetorno = respostaCobol.Substring(5, 30).Trim();
                string statusRetorno = respostaCobol.Substring(35, 2);
                if (statusRetorno == "00")
                {
                    return Ok(new
                    {
                        CliId = idRetorno,
                        CliNome = nomeRetorno,
                        StatusRetorno = statusRetorno
                    });
                }
                else if (statusRetorno == "44")
                {
                    return NotFound(new { erro = "Cliente não encontrado.", status = "44" });
                }
                
                return StatusCode(500, new { erro = "Erro interno." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Falha no .dll: " + ex.Message });
            }
        }
    }
}