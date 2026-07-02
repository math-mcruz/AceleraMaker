namespace dotnet.DTOs.Clientes
{
    public class ClienteUpdateDTO
    {
        public int CLI_Id { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? StatusRetorno { get; set; }
    }
}