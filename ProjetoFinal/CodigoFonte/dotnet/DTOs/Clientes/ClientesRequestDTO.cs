using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteRequestDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(30)]
        public string? Cli_Nome { get; set; }
        [StringLength(18)]
        public string? Telefone { get; set; }
        [StringLength(30)]
        public string? Email { get; set; }
    }
}