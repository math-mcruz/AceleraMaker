using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteUpdateDTO
    {
        [Required(ErrorMessage = "Id é obrigatório.")]
        [StringLength(5)]
        public int Cli_Id { get; set; }
        [StringLength(18)]
        public string? Telefone { get; set; }
        [StringLength(30)]
        public string? Email { get; set; }
    }
}