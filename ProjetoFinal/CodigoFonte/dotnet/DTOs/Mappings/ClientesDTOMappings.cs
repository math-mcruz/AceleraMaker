using dotnet.DTOs.Clientes;

namespace BlogPessoal.DTOs.Mappings;

public static class ClientesDTOMappings
{
    //entrada dos dados para mandar pro banco
    public static ClienteResponseDTO? ToResponse(int cli_id, string cli_nome, string telefone, string email)
    {
        return new ClienteResponseDTO
        {
            Cli_Id = cli_id,
            Cli_Nome = cli_nome, 
            Telefone = telefone,
            Email = email,
        };
    }
}