using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteUpdateDTO
    {
        [Required(ErrorMessage = "Id é obrigatório.")]
        [Range(1, 99999, ErrorMessage = "O ID deve estar entre 1 e 99999")]
        public int Cli_Id { get; set; }
        [StringLength(18)]
        public string? Telefone { get; set; }
        [StringLength(30)]
        public string? Email { get; set; }
    }
}