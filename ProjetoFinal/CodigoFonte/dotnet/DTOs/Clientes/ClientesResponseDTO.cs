namespace dotnet.DTOs.Clientes
{
    public class ClienteResponseDTO
    {
        public int CLI_Id { get; set; }
        public string? CLI_Nome { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? StatusRetorno { get; set; }
    }
}