using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteRequestDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 30 caracteres.")]
        public string? Cli_Nome { get; set; }
        [StringLength(18, ErrorMessage = "O telefone deve ter entre 1 e 18 caracteres.")]
        public string? Telefone { get; set; }
        [StringLength(30, ErrorMessage = "O e-mail deve ter entre 1 e 30 caracteres.")]
        public string? Email { get; set; }
    }
}