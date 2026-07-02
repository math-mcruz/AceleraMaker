using dotnet.DTOs.Clientes;

namespace BlogPessoal.DTOs.Mappings;

public static class ClientesDTOMappings
{
    //entrada dos dados para mandar pro banco
    public static ClienteResponseDTO? ToResponse(this ClienteRequestDTO? clienteRequestDTO, string statusRetorno)
    {
        if (clienteRequestDTO == null) return null;

        return new ClienteResponseDTO
        {
            CLI_Id = clienteRequestDTO.CLI_Id,
            CLI_Nome = clienteRequestDTO.CLI_Nome,
            Telefone = clienteRequestDTO.Telefone,
            Email = clienteRequestDTO.Email,
            StatusRetorno = statusRetorno
        };
    }
    public static ClienteUpdateDTO? ToUpdate(this ClienteRequestDTO? clienteRequestDTO, string statusRetorno)
    {
        if (clienteRequestDTO == null) return null;

        return new ClienteUpdateDTO
        {
            CLI_Id = clienteRequestDTO.CLI_Id,
            Telefone = clienteRequestDTO.Telefone,
            Email = clienteRequestDTO.Email,
        };
    }

    public static ClienteResponseDTO? UpdateToResponse(this ClienteUpdateDTO? clienteUpdatetDTO, string statusRetorno)
    {
        if (clienteUpdatetDTO == null) return null;

        return new ClienteResponseDTO
        {
            CLI_Id = clienteUpdatetDTO.CLI_Id,
            Telefone = clienteUpdatetDTO.Telefone,
            Email = clienteUpdatetDTO.Email,
            StatusRetorno = statusRetorno
        };
    }
}