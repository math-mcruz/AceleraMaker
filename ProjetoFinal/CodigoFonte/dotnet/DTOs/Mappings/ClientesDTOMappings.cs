using dotnet.DTOs.Clientes;

namespace BlogPessoal.DTOs.Mappings;

public static class ClientesDTOMappings
{
    //entrada dos dados para mandar pro banco
    public static ClienteResponseDTO? RequestToResponse(this ClienteRequestDTO? clienteRequestDTO, int cli_id)
    {
        if (clienteRequestDTO == null) return null;

        return new ClienteResponseDTO
        {
            Cli_Id = cli_id,
            Cli_Nome = clienteRequestDTO.Cli_Nome,
            Telefone = clienteRequestDTO.Telefone,
            Email = clienteRequestDTO.Email,
        };
    }
    public static ClienteUpdateDTO? RequestToUpdate(this ClienteRequestDTO? clienteRequestDTO, int cli_id)
    {
        if (clienteRequestDTO == null) return null;

        return new ClienteUpdateDTO
        {
            Cli_Id = cli_id,
            Telefone = clienteRequestDTO.Telefone,
            Email = clienteRequestDTO.Email,
        };
    }

    public static ClienteResponseDTO? UpdateToResponse(this ClienteUpdateDTO? clienteUpdatetDTO, string cli_nome)
    {
        if (clienteUpdatetDTO == null) return null;

        return new ClienteResponseDTO
        {
            Cli_Id = clienteUpdatetDTO.Cli_Id,
            Cli_Nome = cli_nome, 
            Telefone = clienteUpdatetDTO.Telefone,
            Email = clienteUpdatetDTO.Email,
        };
    }
}