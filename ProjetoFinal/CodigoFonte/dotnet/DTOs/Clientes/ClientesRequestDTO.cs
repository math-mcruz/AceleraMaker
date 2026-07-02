using System.ComponentModel.DataAnnotations;

namespace dotnet.DTOs.Clientes
{
    public class ClienteRequestDTO
    {
        [Required(ErrorMessage = "ID é obrigatório.")]
        public int CLI_Id { get; set; }
        //? POR ENQUANTO
        [Required(ErrorMessage = "Nome é obrigatório.")]
        [StringLength(30)]
        public string? CLI_Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
    }
}