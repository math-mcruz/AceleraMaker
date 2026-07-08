using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteUpdateDTO
    {
        [Required(ErrorMessage = "Id é obrigatório.")]
        [Range(1, 99999, ErrorMessage = "O ID deve estar entre 1 e 99999")]
        public int Cli_Id { get; set; }
        [StringLength(18, ErrorMessage = "O telefone deve ter entre 1 e 18 caracteres.")]
        public string? Telefone { get; set; }
        [StringLength(30, ErrorMessage = "O e-mail deve ter entre 1 e 30 caracteres.")]
        public string? Email { get; set; }
    }
}