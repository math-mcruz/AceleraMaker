using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteRequestDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(30)]
        public string? Cli_Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
    }
}